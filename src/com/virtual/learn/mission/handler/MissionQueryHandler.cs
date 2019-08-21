using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using mission.Context;
using mission.Exceptions;
using mission.Models;
using mission.Models.Search;
using MongoDB.Bson;
using MongoDB.Driver;
using SerilogTimings;

namespace mission.Handler
{
    internal class MissionQueryHandler
    {
        private ILogger logger;
        private MissionDbContext context;
        private IConfiguration configuration;

        public MissionQueryHandler(ILogger logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.context = new MissionDbContext(logger);
            this.configuration = configuration;
        }

        internal async Task<Mission> GetMissionAsync(string id)
        {
            SearchMissionCriterias criterias = new SearchMissionCriterias() {
                Id = id
            };
            List<Mission> missions = await ListMissionAsync(criterias);
            if (missions.Any())
            {
                return missions[0];
            }
            throw new UnknownMissionException();
        }

        internal async Task<Mission> AddMissionAsync(Mission mission)
        {
            mission.MissionId = Guid.NewGuid().ToString();
            
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var op = Operation.Begin("Insertion du compte {id}", mission.MissionId)) 
            {
                await context.Context.Database.GetCollection<Mission>("mission").InsertOneAsync(mission, null, token);
                op.Complete();
                return mission;
            }
        }

        internal async Task<bool> UpdateMissionAsync(Mission mission)
        {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var op = Operation.Begin("Mise a jour de la mission {id}", mission.MissionId)) 
            {
                ReplaceOneResult result = await context.Context.Database.GetCollection<Mission>("mission").ReplaceOneAsync(new BsonDocument{{"missionId", mission.MissionId}}, mission, null);
                op.Complete();
                logger.LogInformation("Résultat de la mise à jour de la mission {id} : {result}", mission.MissionId, result.ToJson());
                return result.IsAcknowledged;
            }
        }

        internal async Task<bool> DeleteMissionAsync(string id)
        {
            // on appelle le GetMission pour valider que la mission que l'on souhaite supprimer existe bien
            await GetMissionAsync(id);
            
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var op = Operation.Begin("Suppression de la mission {id}", id)) 
            {
                DeleteResult result = await context.Context.Database.GetCollection<Mission>("mission").DeleteOneAsync(new BsonDocument { { "missionId", id } }, token);
                op.Complete();
                logger.LogInformation("Résultat de la suppression de la mission {id} : {result}", id, result.ToJson());
                return result.IsAcknowledged;
            }
        }

        internal async Task<List<Mission>> ListMissionAsync(SearchMissionCriterias criterias)
        {
            List<Mission> missions = new List<Mission>();
            FilterDefinitionBuilder<Mission> builder = Builders<Mission>.Filter;

            FilterDefinition<Mission> filters = AddCriterias(builder, criterias);

            using (IAsyncCursor<Mission> cursor = await context.Context.Database.GetCollection<Mission>("mission").FindAsync<Mission>(filters))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<Mission> batch = cursor.Current;
                    foreach (Mission document in batch)
                    {
                        missions.Add(document);
                    }
                }
            }
            return missions;
        }

        /// <summary> Creation de la plage de criteres</summary>
        /// <param name="criterias">Criteres a prendre en compte</param>
        /// <returns>FilterDefinition<Mission> : les filtres a prendre en compte</returns>
        private FilterDefinition<Mission> AddCriterias(FilterDefinitionBuilder<Mission> builder, SearchMissionCriterias criterias)
        {
            FilterDefinition<Mission> filt = builder.Empty;
            if (!String.IsNullOrEmpty(criterias.Id))
            {
                filt = builder.Eq("MissionId", criterias.Id);
            }
            if (!String.IsNullOrEmpty(criterias.AgencyAccountId))
            {
                filt = filt & builder.Regex("Agency", criterias.AgencyAccountId);
            }
            // TODO a faire apres la fonctionnalite d'assignation de candidats
            // if (!String.IsNullOrEmpty(criterias.CandidateAccountId))
            // {
            //     filt = filt & builder.Regex("LastName", "/" + criterias.LastName + "/");
            // }
            //TODO a faire apres la fonctionnalite de recherche d'entreprise a la creation de mission
            // if (!String.IsNullOrEmpty(criterias.CompanyAccountId))
            // {
            //     filt = filt & builder.Regex("LastName", "/" + criterias.LastName + "/");
            // }
            return filt;
        }
    }
}
