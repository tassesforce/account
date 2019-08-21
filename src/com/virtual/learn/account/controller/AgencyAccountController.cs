using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using compte.Accounts.List;
using compte.Exceptions;
using compte.Handler.Accounts;
using compte.Models.Accounts;
using compte.Models.Accounts.Enum;
using compte.Models.POC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SerilogTimings;

namespace compte.Controllers.Accounts
{
    /*
     * Controller de gestion des utilisateurs 
     */
     [ApiController]
    public class AgencyAccountController : AccountsController<AgencyAccount>
    {
       
        public AgencyAccountController(IConfiguration configuration, ILogger<AgencyAccountController> logger) : base(configuration, logger)
        {
        }

        //TODO a supprimer apres le POC
        [HttpPost]
        [Route("/v1/account/datas/poc")]
        public IActionResult postDatasPOC([FromBody] DatasPOC datas)
        {
            logger.LogInformation("Récupération des données : " + datas.ToString());
            return Ok();
        }
        
        /// <summary>Methode GET de recuperation des comptes</summary>
        /// <param name="id">Identifiant de l'utilisateur pour lequel recuperer les comptes</param>
        /// <returns>Les informations du compte de l'utilisateur</returns>
        [Authorize(Roles="getAgencyAccount")]
        [HttpGet]
        [Route("/v1/account/agency/{Id}")]
        [ProducesResponseType(200, Type = typeof(AgencyAccount))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> getAgencyAccountAsync([FromRoute] string id)
        {
            return await getAccountAsync(id, AccountTypeEnum.AGENCY.Value);
        }

        /// <summary>Methode POST d'ajout de compte utilisateur'</summary>
        /// <param name="id">Identifiant de l'utilisateur a ajouter</param>
        /// <param name="account">Compte a ajouter</param>
        [Authorize(Roles="postAgencyAccount")]
        [HttpPost]
        [Route("/v1/account/agency/{Id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddAgencyAccount([FromRoute] string id, [FromBody] AgencyAccount account)
        {
            account.AccountType = AccountTypeEnum.AGENCY.Value;
            return await AddAccount(id, account, AccountTypeEnum.AGENCY.Value);
        }

        /// <summary>Methode PUT de mise à jour de compte utilisateur'</summary>
        /// <param name="id">Identifiant de l'utilisateur a mettre a jour</param>
        /// <param name="account">Compte a mettre a jour</param>
        [Authorize(Roles="putAgencyAccount")]
        [HttpPut]
        [Route("/v1/account/agency/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAgencyAccount([FromRoute] string id, [FromBody] AgencyAccount account)
        {
            account.AccountType = AccountTypeEnum.AGENCY.Value;
            return await UpdateAccount(id, account, AccountTypeEnum.AGENCY.Value);
        }

        /// <summary>Methode DELETE de suppression de compte utilisateur</summary>
        /// <param name="id">Identifiant de l'utilisateur a supprimer</param>
        [Authorize(Roles="deleteAgencyAccount")]
        [HttpDelete]
        [Route("/v1/account/agency/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAgencyAccount([FromRoute] string id)
        {
            return await DeleteAccount(id, AccountTypeEnum.AGENCY.Value);
        }

        /// <summary>Methode de recuperation des comptes</summary>
        /// <param name="criterias">criteres de recherche</param>
        /// <returns>Les informations de tous les comptes de type agence</returns>
        [Authorize(Roles="listAccounts")]
        [HttpGet]
        [Route("/v1/account/agency")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> getAgencyAccountsAsync([FromQuery] string name, 
                            [FromQuery] string sector, [FromQuery] string town, [FromQuery] string county, [FromQuery] string postalCode)
        {
            ListAccountsRequest criterias = new ListAccountsRequest {
                    AccountType = AccountTypeEnum.AGENCY.Value,
                    County = county,
                    Name = name,
                    PostalCode = postalCode,
                    Sector = sector,
                    Town = town
                };
            return await getAccountsAsync(criterias);
        }

        /// <summary>Methode de recuperation des comptes</summary>
        /// <param name="id">Identifiant de l'utilisateur pour lequel recuperer les comptes</param>
        /// <returns>Les informations du compte de l'utilisateur</returns>
        public override async Task<IActionResult> getAccountAsync(string id, string accountType)
        {
            using (Operation.Time("Demande d'informations sur le compte id = {id}", id)) 
            {
                if (String.IsNullOrEmpty(id))
                {
                    throw new InvalidRequestException("Un identifiant est obligatoire pour cette recherche");
                }

                AccountQueryHandler<AgencyAccountWithCollaborator> handler = new AccountQueryHandler<AgencyAccountWithCollaborator>(logger, configuration);
                ListAccountsRequest criterias = new ListAccountsRequest{
                    AccountType = accountType
                };

                IList<AgencyAccountWithCollaborator> accounts = await handler.GetAccountsAsync(id, criterias);
                if (accounts.Any()) {
                    AgencyAccountWithCollaborator agency = accounts[0];
                    agency.Collaborators = await handler.GetCollaboratorAccountsAsync(agency.UserId, AccountTypeEnum.AGENCY.Value);
                    return Ok(agency.DecryptAccount(configuration));
                }
                logger.LogError("Le compte {id} n'existe pas", id);
                throw new UnknownAccountException("Le compte " + id + " n'existe pas");
                
            }
        }

    }
}