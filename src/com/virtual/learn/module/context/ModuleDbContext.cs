using compte.Models.Accounts;
using lug.Context.Mongo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace compte.Context.Modules
{
    /// <summary>Context for the modules</summary>
    public class ModuleDbContext : DbContext
    {
        public MongoDbContext<Account> Context {get; private set;}
        public ILogger logger;

        public ModuleDbContext(ILogger logger)
        {
            MongoDbContextFactory<Account> factory = new MongoDbContextFactory<Account>();
            Context = MongoDbContextFactory<Account>.InitializeContext(logger, "mongodb://fcollonge:Ofw7laUGHNcbwBjSaCql@127.0.0.1:27017/module_db", "module_db", "module");
        }

    }
}