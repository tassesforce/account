using System.Threading.Tasks;
using compte.Accounts.List;
using compte.Models.Accounts;
using lug.Context.Mongo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace compte.Context.Filiation
{
    public class FiliationDbContext : DbContext
    {
        public MongoDbContext<AccountFiliation> Context {get; private set;}
        public ILogger logger;

        public FiliationDbContext(ILogger logger)
        {
            MongoDbContextFactory<AccountFiliation> factory = new MongoDbContextFactory<AccountFiliation>();
            Context = MongoDbContextFactory<AccountFiliation>.InitializeContext(logger, "mongodb://fcollonge:Ofw7laUGHNcbwBjSaCql@127.0.0.1:27017/account_db", "account_db", "account_filiation");
        }

    }
}