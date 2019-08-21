using System.Threading.Tasks;
using compte.Models.Accounts;
using lug.Context.Mongo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using mission.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mission.Context
{
    public class MissionDbContext : DbContext
    {
        public MongoDbContext<Mission> Context {get; private set;}
        public ILogger logger;

        public MissionDbContext(ILogger logger)
        {
            MongoDbContextFactory<Account> factory = new MongoDbContextFactory<Account>();
            Context = MongoDbContextFactory<Mission>.InitializeContext(logger, "mongodb://fcollonge:Ofw7laUGHNcbwBjSaCql@127.0.0.1:27017/account_db", "mission_db", "mission");
        }

    }
}