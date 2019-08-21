using System.Collections.Generic;
using System.Threading.Tasks;
using compte.Context.Modules;
using compte.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace compte.Handler.Modules 
{
    /// <summary>Access to the datas of the modules</summary>
    public class ModuleQueryHandler
    {
        private ILogger logger;
        private ModuleDbContext context;
        private IConfiguration configuration;

        public ModuleQueryHandler(ILogger logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.context = new ModuleDbContext(logger);
            this.configuration = configuration;
        }

        /// <summary>Returns a list of modules corresponding to the criterias in parameter</summary>
        /// <param name="criterias">criterias used for searching modules</param>
        /// <returns>La liste des comptes correspondant au userId.async Peut aussi retourner la liste de tous les comptes si id est vide</returns>
        internal async Task<IList<Module>> GetModulesAsync(ListModulesRequest criterias)
        {
            IList<Module> modules = new List<Module>();
            FilterDefinition<Module> filters = createFilters(criterias);

            using (IAsyncCursor<Module> cursor = await context.Context.Database.GetCollection<Module>("module").FindAsync<Module>(filters))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<Module> batch = cursor.Current;
                    logger.LogInformation("Entrée dans la boucle de récupération des modules");
                    foreach (Module document in batch)
                    {

                        logger.LogInformation("Module : " + document.ToString());
                        modules.Add(document);
                    }
                }
            }
            return modules;
        }

        /// <summary>Create the filters to search modules</summary>
        /// <param name="criterias">Criterias used for create the filters</param>
        /// <returns>FilterDefinition<Module> : filters used for searching modules </returns>
        private FilterDefinition<Module> createFilters(ListModulesRequest criterias)
        {
            
            FilterDefinitionBuilder<Module> builder = Builders<Module>.Filter;
            FilterDefinition<Module> filt = builder.Empty;

            if (!string.IsNullOrEmpty(criterias.ModuleId))
            {
                filt = filt & builder.Eq("ModuleId", criterias.ModuleId);
            }
            if (!string.IsNullOrEmpty(criterias.UserId))
            {
                filt = filt & builder.Eq("UserId", criterias.UserId);
            }
            if (!string.IsNullOrEmpty(criterias.Label))
            {
                filt = filt & builder.Regex("Label", "/" + criterias.Label + "/gi");
            }
            if (!string.IsNullOrEmpty(criterias.Type))
            {
                filt = filt & builder.Eq("Type", criterias.Type);
            }
            if (criterias.Length != null)
            {
                filt = filt & builder.Eq("Length", criterias.Length);
            }
            if (criterias.NbCredits != null)
            {
                filt = filt & builder.Eq("NbCredits", criterias.NbCredits);
            }
            
            return filt;
        }
    }
}