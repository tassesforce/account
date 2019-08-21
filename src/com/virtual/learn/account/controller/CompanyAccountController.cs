using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using compte.Accounts.List;
using compte.Context.Accounts;
using compte.Exceptions;
using compte.Handler.Accounts;
using compte.Models.Accounts;
using compte.Models.Accounts.Enum;
using lug.String.Encrypt;
using Lug.GlobalErrorHandling.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SerilogTimings;

namespace compte.Controllers.Accounts
{
    /*
     * Controller de gestion des utilisateurs 
     */
    [ApiController]
    public class CompanyAccountController : AccountsController<CompanyAccount>
    {
        public CompanyAccountController(IConfiguration configuration, ILogger<AgencyAccountController> logger) : base(configuration, logger)
        {
        }
        
        /// <summary>Methode GET de recuperation des comptes</summary>
        /// <param name="id">Identifiant de l'utilisateur pour lequel recuperer les comptes</param>
        /// <returns>Les informations du compte de l'utilisateur</returns>
        [Authorize(Roles="getCompanyAccount")]
        [HttpGet]
        [Route("/v1/account/company/{Id}")]
        [ProducesResponseType(200, Type = typeof(CompanyAccount))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> getCompanyAccountAsync([FromRoute] string id)
        {
            return await getAccountAsync(id, AccountTypeEnum.COMPANY.Value);
        }

        /// <summary>Methode POST d'ajout de compte utilisateur'</summary>
        /// <param name="id">Identifiant de l'utilisateur a ajouter</param>
        /// <param name="account">Compte a ajouter</param>
        [Authorize(Roles="postCompanyAccount")]
        [HttpPost]
        [Route("/v1/account/company/{Id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddCompanyAccount([FromRoute] string id, [FromBody] CompanyAccount account)
        {
            account.AccountType = AccountTypeEnum.COMPANY.Value;
            return await AddAccount(id, account, AccountTypeEnum.COMPANY.Value);
        }

        /// <summary>Methode PUT de mise à jour de compte utilisateur'</summary>
        /// <param name="id">Identifiant de l'utilisateur a mettre a jour</param>
        /// <param name="account">Compte a mettre a jour</param>
        [Authorize(Roles="putCompanyAccount")]
        [HttpPut]
        [Route("/v1/account/company/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAccount([FromRoute] string id, [FromBody] CompanyAccount account)
        {
            account.AccountType = AccountTypeEnum.COMPANY.Value;
            return await UpdateAccount(id, account, AccountTypeEnum.COMPANY.Value);
        }

        /// <summary>Methode DELETE de suppression de compte utilisateur</summary>
        /// <param name="id">Identifiant de l'utilisateur a supprimer</param>
        [Authorize(Roles="deleteCompanyAccount")]
        [HttpDelete]
        [Route("/v1/account/company/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCompanyAccount([FromRoute] string id)
        {
            return await DeleteAccount(id, AccountTypeEnum.COMPANY.Value);
        }

        /// <summary>Methode de recuperation des comptes</summary>
        /// <param name="criterias">criteres de recherche</param>
        /// <returns>Les informations de tous les comptes de type agence</returns>
        [Authorize(Roles="listAccounts")]
        [HttpGet]
        [Route("/v1/account/company")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> getCompanyAccountsAsync([FromQuery] string name, 
                            [FromQuery] string sector, [FromQuery] string town, [FromQuery] string county, [FromQuery] string postalCode)
        {
            ListAccountsRequest criterias = new ListAccountsRequest {
                    AccountType = AccountTypeEnum.COMPANY.Value,
                    County = county,
                    Name = name,
                    PostalCode = postalCode,
                    Sector = sector,
                    Town = town
                };
            return await getAccountsAsync(criterias);
        }

        
    }
}