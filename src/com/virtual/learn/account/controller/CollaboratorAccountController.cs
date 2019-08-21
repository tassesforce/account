using System.Collections.Generic;
using System.Threading.Tasks;
using compte.Accounts.Collaborator;
using compte.Handler.Accounts;
using compte.Models.Accounts;
using compte.Models.Accounts.Enum;
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
    public class CollaboratorAccountController : AccountsController<CollaboratorAccount>
    {
       
        public CollaboratorAccountController(IConfiguration configuration, ILogger<CollaboratorAccountController> logger) : base(configuration, logger)
        {
        }
        
        /// <summary>Methode GET de recuperation des comptes</summary>
        /// <param name="id">Identifiant de l'utilisateur pour lequel recuperer les comptes</param>
        /// <returns>Les informations du compte de l'utilisateur</returns>
        [Authorize(Roles="getCollaboratorAccount")]
        [HttpGet]
        [Route("/v1/account/collaborator/{Id}")]
        [ProducesResponseType(200, Type = typeof(AgencyAccount))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> getCollaboratorAccountAsync([FromRoute] string id)
        {
            return await getAccountAsync(id, AccountTypeEnum.COLLABORATOR.Value);
        }

        /// <summary>Methode POST d'ajout de compte utilisateur'</summary>
        /// <param name="id">Identifiant de l'utilisateur a ajouter</param>
        /// <param name="account">Compte a ajouter</param>
        [Authorize(Roles="postCollaboratorAccount")]
        [HttpPost]
        [Route("/v1/account/collaborator/{Id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddCollaboratorAccount([FromRoute] string id, [FromBody] CollaboratorAccount account)
        {
            account.AccountType = AccountTypeEnum.COLLABORATOR.Value;
            return await AddAccount(id, account, AccountTypeEnum.COLLABORATOR.Value);
        }

        /// <summary>Methode PUT de mise à jour de compte utilisateur'</summary>
        /// <param name="id">Identifiant de l'utilisateur a mettre a jour</param>
        /// <param name="account">Compte a mettre a jour</param>
        [Authorize(Roles="putCollaboratorAccount")]
        [HttpPut]
        [Route("/v1/account/collaborator/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCollaboratorAccount([FromRoute] string id, [FromBody] CollaboratorAccount account)
        {
            account.AccountType = AccountTypeEnum.COLLABORATOR.Value;
            return await UpdateAccount(id, account, AccountTypeEnum.COLLABORATOR.Value);
        }

        /// <summary>Methode DELETE de suppression de compte utilisateur</summary>
        /// <param name="id">Identifiant de l'utilisateur a supprimer</param>
        [Authorize(Roles="deleteCollaboratorAccount")]
        [HttpDelete]
        [Route("/v1/account/collaborator/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCollaboratorAccount([FromRoute] string id)
        {
            return await DeleteAccount(id, AccountTypeEnum.COLLABORATOR.Value);
        }

        /// <summary>GET all the collaborators of a referent account</summary>
        /// <param name="id">Id of the referent account</param>
        /// <param name="accountType">Type of the referent account</param>
        /// <returns>List of the collaborators of a referent account</returns>
        [Authorize(Roles="listCollaboratorAccounts")]
        [HttpGet]
        [Route("/v1/account/collaborator")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> getCollaboratorAccountsAsync([FromQuery] string id, [FromQuery] string accountType)
        {

            using (Operation.Time("Demande de liste de tous les comptes agence")) 
            {
                AccountQueryHandler<CollaboratorAccount> handler = new AccountQueryHandler<CollaboratorAccount>(logger, configuration);
                
                IList<CollaboratorAccount> encryptedAccounts = await handler.GetCollaboratorAccountsAsync(id, accountType);
                IList<CollaboratorAccount> decryptedAccounts = DecryptAccounts(encryptedAccounts);
                
                return Ok(decryptedAccounts);
            }
        }

    }
}