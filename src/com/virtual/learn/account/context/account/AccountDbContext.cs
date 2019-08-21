using System.Threading.Tasks;
using compte.Models.Accounts;
using lug.Context.Mongo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace compte.Context.Accounts
{
    public class AccountDbContext : DbContext
    {
        public MongoDbContext<Account> Context {get; private set;}
        public ILogger logger;

        public AccountDbContext(ILogger logger)
        {
            MongoDbContextFactory<Account> factory = new MongoDbContextFactory<Account>();
            Context = MongoDbContextFactory<Account>.InitializeContext(logger, "mongodb://fcollonge:Ofw7laUGHNcbwBjSaCql@127.0.0.1:27017/account_db", "account_db", "account");
        }

    }
}