using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using compte.Accounts.Collaborator;
using compte.Accounts.List;
using compte.Context.Accounts;
using compte.FIlters.Accounts;
using compte.Models.Accounts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SerilogTimings;

namespace compte.Handler.Accounts 
{
    public class AccountQueryHandler<T> where T : Account
    {
        private ILogger logger;
        private AccountDbContext context;
        private IConfiguration configuration;

        public AccountQueryHandler(ILogger logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.context = new AccountDbContext(logger);
            this.configuration = configuration;
        }

        /// <summary>Retourne les comptes lies a un userId.async Peut aussi retourner tous les comptes si l'id est vide</summary>
        /// <param name="id">UserId a rechercher</param>
        /// <param name="criterias">Criteres de recherche</param>
        /// <returns>La liste des comptes correspondant au userId.async Peut aussi retourner la liste de tous les comptes si id est vide</returns>
        internal async Task<IList<T>> GetAccountsAsync(string id, ListAccountsRequest criterias)
        {
            List<T> accountsFromMongo = new List<T>();
            FilterDefinition<T> filters = createFilters(id, criterias);

            using (IAsyncCursor<T> cursor = await context.Context.Database.GetCollection<T>("account").FindAsync<T>(filters))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<T> batch = cursor.Current;
                    foreach (T document in batch)
                    {
                        accountsFromMongo.Add(document);
                    }
                }
            }
            if (hasCipheredFilters(criterias))
            {
                return filterAccountsWithCipheredFilters(accountsFromMongo, criterias);
            }
            return accountsFromMongo;
        }

        /// <summary>Ajout d'un compte en base</summary>
        /// <param name="account">Compte a ajouter</param>
        /// <param name="id">Identifiant du compte</param>
        /// <returns>T : compte apres enregistrement</returns>
        internal async Task<T> AddAccountAsync(string id, T account)
        {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var op = Operation.Begin("Insertion du compte {id}", id)) 
            {
                await context.Context.Database.GetCollection<T>("account").InsertOneAsync(account, null, token);
                op.Complete();
                return account;
            }
        }

        /// <summary>Mise a jour d'un compte</summary>
        /// <param name="account">Compte a mettre a jour</param>
        /// <param name="id">identifiant du compte a mettre a jour</param>
        /// <returns>bool : booleen qui indique si l'operation s'est deroulee</returns>
        internal async Task<bool> UpdateAccountAsync(string id, T account)
        {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var op = Operation.Begin("Suppression du compte {id}", id)) 
            {
                ReplaceOneResult result = await context.Context.Database.GetCollection<T>("account").ReplaceOneAsync(new BsonDocument{{"UserId", id}}, account, null);
                op.Complete();
                logger.LogInformation("Résultat de la mise à jour du compte {id} : {result}", id, result.ToJson());
                return result.IsAcknowledged;
            }
        }

        /// <summary> Suppression d'un compte</summary>
        /// <param name="id">identifiant du compte a supprimer</param>
        /// <returns>bool : booleen qui indique si l'operation s'est deroulee</returns>
        internal async Task<bool> DeleteAccountAsync(string id)
        {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var op = Operation.Begin("Suppression du compte {id}", id)) 
            {
                DeleteResult result = await context.Context.Database.GetCollection<T>("account").DeleteOneAsync(new BsonDocument { { "UserId", id } }, token);
                op.Complete();
                logger.LogInformation("Résultat de la suppression du compte {id} : {result}", id, result.ToJson());
                return result.IsAcknowledged;
            }
        }

        /// <summary>Get all the collaborators of a referent account</summary>
        /// <param name="id">Id of the referent account</param>
        /// <param name="accountType">Type of the referent account</param>
        /// <return>List<CollaboratorAccount> : list of the collaborator<return>
        internal async Task<List<CollaboratorAccount>> GetCollaboratorAccountsAsync(string id, string accountType)
        {
            List<CollaboratorAccount> accounts = new List<CollaboratorAccount>();
            FilterDefinitionBuilder<CollaboratorAccount> builder = Builders<CollaboratorAccount>.Filter;
            // FilterDefinition<CollaboratorAccount> filters = builder.Eq("Referents", {"UserId:" + id + ", AccountType: " + accountType + "});
            FilterDefinition<CollaboratorAccount> filters = builder.Eq("Referents.UserId", id);
            filters = filters & builder.Eq("AccountType", "collaborator");

            using (IAsyncCursor<CollaboratorAccount> cursor = await context.Context.Database.GetCollection<CollaboratorAccount>("account").FindAsync<CollaboratorAccount>(filters))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<CollaboratorAccount> batch = cursor.Current;
                    foreach (CollaboratorAccount document in batch)
                    {
                        accounts.Add(document);
                    }
                }
            }
            return accounts;
        }

        /// <summary>Generation des criteres de recherche de comptes</summary>
        /// <param name="criterias">Criteres a prendre en compte</param>
        /// <param name="id">Critere identifiant de compte</param>
        /// <returns>FilterDefinition<T> : filtres a appliquer sur la recherche</returns>
        private FilterDefinition<T> createFilters(string id, ListAccountsRequest criterias)
        {
            // Les filtres ne concernent pas l'integralite des criteres entres en parametres, car ils sont stockes de maniere chiffree. Il faut donc passer
            // par une etape supplementaire pour les prendre en compte
            FilterDefinitionBuilder<T> builder = Builders<T>.Filter;
            FilterDefinition<T> filt = builder.Eq("AccountType", criterias.AccountType);

            if (!String.IsNullOrEmpty(id))
            {
                filt = filt & builder.Eq("UserId", id);
            }
            if (!String.IsNullOrEmpty(criterias.Sector)) 
            {
                filt = filt & builder.Eq("Sector", criterias.Sector);
            }
            if (!String.IsNullOrEmpty(criterias.DispoDate))
            {
                filt = filt & builder.Eq("DispoDate", criterias.DispoDate);
            }

            return filt;
        }

        /// <summary>Permet de savoir si la recherche porte sur des elements personnels du compte (donc chiffres)</summary>
        /// <param name="criterias">Criteres de recherche</param>
        /// <returns>bool : true si la recherche porte sur un element chiffre, false sinon</returns>
        private bool hasCipheredFilters(ListAccountsRequest criterias)
        {
            // on regarde si la recherche porte sur un element personnel et donc chiffre du compte ou pas
            if (!string.IsNullOrEmpty(criterias.County)
                || !string.IsNullOrEmpty(criterias.PostalCode)
                || !string.IsNullOrEmpty(criterias.Town)
                || !string.IsNullOrEmpty(criterias.Name)
                || !string.IsNullOrEmpty(criterias.FirstName)
                || !string.IsNullOrEmpty(criterias.LastName))
                return true;

            return false;
        }

        /// <summary> Creation de la plage de criteres concernant la partie nom (raison sociale ou nom de candidat)</summary>
        /// <param name="criterias">Criteres a prendre en compte</param>
        /// <returns>FilterDefinition<T> : les filtres a prendre en compte pour la partie nom</returns>
        private FilterDefinition<T> AddCriteriaName(FilterDefinitionBuilder<T> builder, ListAccountsRequest criterias)
        {
            FilterDefinition<T> filt = builder.Empty;
            if (!String.IsNullOrEmpty(criterias.Name))
            {
                // filt = builder.Regex("Name", "/" + criterias.Name + "/");
                filt = builder.Eq("Name", criterias.Name);
            }
            if (!String.IsNullOrEmpty(criterias.FirstName))
            {
                filt = filt & builder.Regex("FirstName", "/" + criterias.FirstName + "/");
            }
            if (!String.IsNullOrEmpty(criterias.LastName))
            {
                filt = filt & builder.Regex("LastName", "/" + criterias.LastName + "/");
            }
            return filt;
        }

        /// <summary> Creation de la plage de criteres concernant la partie adresse</summary>
        /// <param name="criterias">Criteres a prendre en compte</param>
        /// <returns>FilterDefinition<T> : les filtres a prendre en compte pour la partie adresse</returns>
        private FilterDefinition<T> AddCriteriaAdress(FilterDefinitionBuilder<T> builder, ListAccountsRequest criterias)
        {
            FilterDefinition<T> filt = builder.Empty;
            if (!String.IsNullOrEmpty(criterias.Town))
            {
                filt = builder.Regex("Town", "/" + criterias.Town + "/");
            }
            if (!String.IsNullOrEmpty(criterias.County))
            {
                filt = filt & builder.Regex("County", "/" + criterias.County + "/");
            }
            if (!String.IsNullOrEmpty(criterias.PostalCode))
            {
                filt = filt & builder.Regex("PostalCode", "/" + criterias.PostalCode + "/");
            }
            return filt;
        }

        private IList<T> filterAccountsWithCipheredFilters(List<T> accountsFromMongo, ListAccountsRequest criterias)
        {
            AccountFilters<T> filters = AccountFiltersFactory<T>.BuildAccountFilters(criterias.AccountType);
            return filters.FilterAccounts(accountsFromMongo, criterias, configuration);
            
        }

    }
}