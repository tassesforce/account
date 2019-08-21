using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using compte.Controller;
using SerilogTimings;
using compte.Modules;
using compte.Handler.Modules;

namespace compte.Controllers.Modules
{
    /// <summary>controller to manage the modules</summary>
    [ApiController]
    public class ModulesController : WebApiController
    {
        protected ILogger<ModulesController> logger;
       
        protected IConfiguration configuration;

        public ModulesController(IConfiguration configuration, ILogger<ModulesController> Logger)
        {
            this.configuration = configuration;
            this.logger = Logger;
        }
        
        /// <summary>Methode de recuperation des comptes</summary>
        /// <param name="criterias">criteres de recherche</param>
        /// <returns>Les informations de tous les comptes de type agence</returns>
        [Authorize(Roles="getModules")]
        [HttpPost]
        [Route("/v1/module")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> getModulesAsync([FromBody] ListModulesRequest criterias)
        {

            using (Operation.Time("Demande de liste des modules selon les criteres : {criterias}", "")) 
            {
                ModuleQueryHandler handler = new ModuleQueryHandler(logger, configuration);
                
                IList<Module> modules = await handler.GetModulesAsync(criterias);
                
                return Ok(modules);
            }
        }

    }
}