using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using compte.Accounts.List;
using compte.Exceptions;
using compte.Handler.Accounts;
using compte.Handler.Filiation;
using compte.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using compte.Controller;
using SerilogTimings;

namespace compte.Controllers.Accounts
{
    /*
     * Controller de gestion des comptes
     */
    [ApiController]
    public abstract class AccountsController<T> : WebApiController where T : Account
    {
        protected ILogger logger;
       
        protected IConfiguration configuration;

        public AccountsController(IConfiguration configuration, ILogger Logger)
        {
            this.configuration = configuration;
            this.logger = Logger;
        }

        /// <summary>Methode POST de gestion des filiations</summary>
        /// <param name="criterias">criteres de recherche</param>
        [Authorize(Roles="postFiliationAccount")]
        [HttpPost]
        [Route("/v1/account/filiation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ManageFiliation([FromBody] AccountFiliation criterias)
        {
            using (Operation.Time("Gestion de la filiation {status} {idParent} => {idEnfant}", criterias.Status, criterias.IdParent, criterias.IdEnfant)) 
            {
                FiliationQueryHandler handler = new FiliationQueryHandler(logger);
                bool result = false;
                if (StatusFiliationEnum.CREATED.Equals(StatusFiliationEnum.ValueOf(criterias.Status)))
                {
                    result = await handler.AddFiliation(criterias);
                } else if (StatusFiliationEnum.VALIDATED.Equals(StatusFiliationEnum.ValueOf(criterias.Status)) 
                        || StatusFiliationEnum.REFUSED.Equals(StatusFiliationEnum.ValueOf(criterias.Status)))
                {
                    result = await handler.UpdateFiliation(criterias);
                } else if (StatusFiliationEnum.REFUSED.Equals(StatusFiliationEnum.ValueOf(criterias.Status)))
                {
                    result = await handler.DeleteFiliation(criterias);
                }
                else
                {
                    logger.LogError("Le status demandé n'est pas un statut de filiation valide (demandé {status}, possible {dispo})", criterias.Status,  string.Join(", ", StatusFiliationEnum.ToList()));
                }
                if (result)
                {
                    return Ok();
                }
                return BadRequest();
            }
        }
        
        /// <summary>Methode de recuperation des comptes</summary>
        /// <param name="criterias">criteres de recherche</param>
        /// <returns>Les informations de tous les comptes de type agence</returns>
        [Authorize(Roles="listAccounts")]
        public async Task<IActionResult> getAccountsAsync(ListAccountsRequest criterias)
        {

            using (Operation.Time("Demande de liste de tous les comptes agence")) 
            {
                AccountQueryHandler<T> handler = new AccountQueryHandler<T>(logger, configuration);
                
                IList<T> encryptedAccounts = await handler.GetAccountsAsync(null, criterias);
                IList<T> decryptedAccounts = DecryptAccounts(encryptedAccounts);
                
                return Ok(decryptedAccounts);
            }
        }

        /// <summary>Methode de recuperation des comptes</summary>
        /// <param name="id">Identifiant de l'utilisateur pour lequel recuperer les comptes</param>
        /// <returns>Les informations du compte de l'utilisateur</returns>
        public virtual async Task<IActionResult> getAccountAsync(string id, string accountType)
        {
            using (Operation.Time("Demande d'informations sur le compte id = {id}", id)) 
            {
                if (String.IsNullOrEmpty(id))
                {
                    throw new InvalidRequestException("Un identifiant est obligatoire pour cette recherche");
                }

                AccountQueryHandler<T> handler = new AccountQueryHandler<T>(logger, configuration);
                ListAccountsRequest criterias = new ListAccountsRequest{
                    AccountType = accountType
                };

                IList<T> accounts = await handler.GetAccountsAsync(id, criterias);
                if (accounts.Any()) {
                    return Ok(accounts[0].DecryptAccount(configuration));
                }
                logger.LogError("Le compte {id} n'existe pas", id);
                throw new UnknownAccountException("Le compte " + id + " n'existe pas");
                
            }
        }

        /// <summary>Methode d'ajout de compte utilisateur'</summary>
        /// <param name="id">Identifiant de l'utilisateur a ajouter</param>
        /// <param name="account">Compte a ajouter</param>
        public async Task<IActionResult> AddAccount(string id, T account, string accountType)
        {
            using (Operation.Time("Ajout du compte {id}", id)) 
            {
                AccountQueryHandler<T> handler = new AccountQueryHandler<T>(logger, configuration);
                ListAccountsRequest criterias = new ListAccountsRequest{
                    AccountType = accountType
                };
                IList<T> accounts = await handler.GetAccountsAsync(id, criterias);
                if (accounts.Any()) {
                    logger.LogError("Le compte {id} est déjà présent dans le référentiel", id);
                    throw new KnownAccountException("Le compte " + id + " est déjà présent dans notre référentiel");
                }

                T persistedAccount = await handler.AddAccountAsync(id, (T) account.EncryptAccount(configuration));
                return Created(configuration["API:Urls:Compte:Prefixe"] + accountType + "/" + id, persistedAccount.DecryptAccount(configuration));
            }
        }

        /// <summary>Methode de mise à jour de compte utilisateur'</summary>
        /// <param name="id">Identifiant de l'utilisateur a mettre a jour</param>
        /// <param name="account">Compte a mettre a jour</param>
        public async Task<IActionResult> UpdateAccount(string id, T account, string accountType)
        {
            using (Operation.Time("Mise à jour du compte {id}", id)) 
            {
                AccountQueryHandler<T> handler = new AccountQueryHandler<T>(logger, configuration);
                ListAccountsRequest criterias = new ListAccountsRequest{
                    AccountType = accountType
                };
                IList<T> accounts = await handler.GetAccountsAsync(id, criterias);
                if (!accounts.Any()) {
                    logger.LogError("Le compte {id} n'existe pas", id);
                    throw new UnknownAccountException("Le compte " + id + " est absent du référentiel");
                }
                bool isAcknowledged = await handler.UpdateAccountAsync(id, (T) account.EncryptAccount(configuration));
                if (isAcknowledged)
                {
                    return Ok();
                }
                return BadRequest();
            }
        }

        /// <summary>Methode de suppression de compte utilisateur</summary>
        /// <param name="id">Identifiant de l'utilisateur a supprimer</param>
        public async Task<IActionResult> DeleteAccount(string id, string accountType)
        {
            using (Operation.Time("Suppression du compte {id}", id)) 
            {
                AccountQueryHandler<T> handler = new AccountQueryHandler<T>(logger, configuration);
                ListAccountsRequest criterias = new ListAccountsRequest{
                    AccountType = accountType
                };
                IList<T> accounts = await handler.GetAccountsAsync(id, criterias);
                if (!accounts.Any()) {
                    logger.LogError("Le compte {id} n'existe pas", id);
                    throw new UnknownAccountException("Le compte " + id + " est absent du référentiel");
                }
                bool isAcknowledged = await handler.DeleteAccountAsync(id);
                if (isAcknowledged)
                {
                    return Ok();
                }
                return BadRequest();
            }
        }

        /// <summary> Dechiffrage d'une liste de comptes</summary>
        /// <param name="encryptedAccounts">Liste des comptes a dechiffrer</param>
        /// <returns>List<T> liste des comptes dechiffres</returns>
        internal List<T> DecryptAccounts(IList<T> encryptedAccounts)
        {
            List<T> decryptedAccounts = new List<T>();
            if (encryptedAccounts != null)
            {
                foreach (T account in encryptedAccounts)
                {
                    decryptedAccounts.Add((T) account.DecryptAccount(configuration));
                }
            }
            return decryptedAccounts;
        }

    }
}