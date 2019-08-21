using System.Threading.Tasks;
using compte.Accounts.List;
using compte.Models.Accounts;
using compte.Models.Accounts.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace compte.Controllers.Accounts
{
    
    /*
     * Controller de gestion des utilisateurs 
     */
    [ApiController]
    public class CandidateAccountController : AccountsController<CandidateAccount>
    {

        public CandidateAccountController(IConfiguration configuration, ILogger<AgencyAccountController> logger) : base(configuration, logger)
        {
        }
        
        /// <summary>Methode GET de recuperation des comptes</summary>
        /// <param name="id">Identifiant de l'utilisateur pour lequel recuperer les comptes</param>
        /// <returns>Les informations du compte de l'utilisateur</returns>
        [Authorize(Roles="getCandidateAccount")]
        [HttpGet]
        [Route("/v1/account/candidate/{Id}")]
        [ProducesResponseType(200, Type = typeof(CandidateAccount))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> getCandidateAccountAsync([FromRoute] string id)
        {
            return await getAccountAsync(id, AccountTypeEnum.CANDIDATE.Value);
        }

        /// <summary>Methode POST d'ajout de compte utilisateur'</summary>
        /// <param name="id">Identifiant de l'utilisateur a ajouter</param>
        /// <param name="account">Compte a ajouter</param>
        [Authorize(Roles="postCandidateAccount")]
        [HttpPost]
        [Route("/v1/account/candidate/{Id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddCandidateAccount([FromRoute] string id, [FromBody] CandidateAccount account)
        {
            account.AccountType = AccountTypeEnum.CANDIDATE.Value;
            return await AddAccount(id, account, AccountTypeEnum.CANDIDATE.Value);
        }

        /// <summary>Methode PUT de mise à jour de compte utilisateur'</summary>
        /// <param name="id">Identifiant de l'utilisateur a mettre a jour</param>
        /// <param name="account">Compte a mettre a jour</param>
        [Authorize(Roles="putCandidateAccount")]
        [HttpPut]
        [Route("/v1/account/candidate/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAccount([FromRoute] string id, [FromBody] CandidateAccount account)
        {
            account.AccountType = AccountTypeEnum.CANDIDATE.Value;
            return await UpdateAccount(id, account, AccountTypeEnum.CANDIDATE.Value);
        }

        /// <summary>Methode DELETE de suppression de compte utilisateur</summary>
        /// <param name="id">Identifiant de l'utilisateur a supprimer</param>
        [Authorize(Roles="deleteCandidateAccount")]
        [HttpDelete]
        [Route("/v1/account/candidate/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCandidateAccount([FromRoute] string id)
        {
            return await DeleteAccount(id, AccountTypeEnum.CANDIDATE.Value);
        }

        /// <summary>Methode de recuperation des comptes</summary>
        /// <param name="criterias">criteres de recherche</param>
        /// <returns>Les informations de tous les comptes de type agence</returns>
        [Authorize(Roles="listAccounts")]
        [HttpGet]
        [Route("/v1/account/candidate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> getCandidateAccountsAsync([FromQuery] string lastName, [FromQuery] string firstName, 
                            [FromQuery] string town, [FromQuery] string county, [FromQuery] string postalCode, [FromQuery] string dispoDate)
        {
            ListAccountsRequest criterias = new ListAccountsRequest {
                    AccountType = AccountTypeEnum.CANDIDATE.Value,
                    County = county,
                    DispoDate = dispoDate,
                    FirstName = firstName,
                    LastName = lastName,
                    PostalCode = postalCode,
                    Town = town
                };
            return await getAccountsAsync(criterias);
        }

    }
}