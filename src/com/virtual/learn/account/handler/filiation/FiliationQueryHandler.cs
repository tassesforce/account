using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using compte.Accounts.List;
using compte.Context.Accounts;
using compte.Context.Filiation;
using compte.Models.Accounts;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SerilogTimings;

namespace compte.Handler.Filiation 
{
    public class FiliationQueryHandler
    {
        private ILogger logger;
        private FiliationDbContext context;

        public FiliationQueryHandler(ILogger logger)
        {
            this.logger = logger;
            this.context = new FiliationDbContext(logger);
        }

        /// <summary>Retourne les comptes lies a un userId.async Peut aussi retourner tous les comptes si l'id est vide</summary>
        /// <param name="id">UserId a rechercher</param>
        /// <param name="criterias">Criteres de recherche</param>
        /// <returns>La liste des comptes correspondant au userId.async Peut aussi retourner la liste de tous les comptes si id est vide</returns>
        internal async Task<bool> AddFiliation(AccountFiliation filiation)
        {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var op = Operation.Begin("Insertion de la filiation {idParent} => {idEnfant}", filiation.IdParent, filiation.IdEnfant)) 
            {
                try 
                {
                    await context.Context.Database.GetCollection<AccountFiliation>("account_filiation").InsertOneAsync(filiation, null, token);
                } catch (System.Exception e)
                {
                    logger.LogError("Erreur lors de l'insertion d'une filiation de comptes", e);
                    return false;
                }
                op.Complete();
                return true;
            }
        }

        /// <summary>Retourne les comptes lies a un userId.async Peut aussi retourner tous les comptes si l'id est vide</summary>
        /// <param name="id">UserId a rechercher</param>
        /// <param name="criterias">Criteres de recherche</param>
        /// <returns>La liste des comptes correspondant au userId.async Peut aussi retourner la liste de tous les comptes si id est vide</returns>
        internal async Task<bool> UpdateFiliation(AccountFiliation filiation)
        {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var op = Operation.Begin("Insertion de la filiation {idParent} => {idEnfant}", filiation.IdParent, filiation.IdEnfant)) 
            {
                await context.Context.Database.GetCollection<AccountFiliation>("account_filiation").ReplaceOneAsync(new BsonDocument{{"IdEnfant", filiation.IdEnfant}, {"IdParent", filiation.IdParent}}, filiation, null, token);
                try 
                {
                    await context.Context.Database.GetCollection<AccountFiliation>("account_filiation").InsertOneAsync(filiation, null, token);
                } catch (System.Exception e)
                {
                    logger.LogError("Erreur lors de l'insertion d'une filiation de comptes", e);
                    return false;
                }
                op.Complete();
                return true;
            }
        }

        /// <summary>Retourne les comptes lies a un userId.async Peut aussi retourner tous les comptes si l'id est vide</summary>
        /// <param name="id">UserId a rechercher</param>
        /// <param name="criterias">Criteres de recherche</param>
        /// <returns>La liste des comptes correspondant au userId.async Peut aussi retourner la liste de tous les comptes si id est vide</returns>
        internal async Task<bool> DeleteFiliation(AccountFiliation filiation)
        {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var op = Operation.Begin("Insertion de la filiation {idParent} => {idEnfant}", filiation.IdParent, filiation.IdEnfant)) 
            {
                DeleteResult result = await context.Context.Database.GetCollection<AccountFiliation>("account_filiation").DeleteOneAsync(new BsonDocument {{"IdEnfant", filiation.IdEnfant}, {"IdParent", filiation.IdParent}}, token);
                op.Complete();
                logger.LogInformation("Résultat de la suppression du compte {idParent} => {idEnfant} : {result}", filiation.IdParent, filiation.IdEnfant, result.ToJson());
                return result.IsAcknowledged;
            }
        }
    }
}