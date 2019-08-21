using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using compte.Controller;
using compte.Models.Accounts.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using mission.Handler;
using mission.Models;
using mission.Models.Search;
using SerilogTimings;

namespace mission.Controller
{
    /// <summary>Controller de gestion des missions</summary>
    [ApiController]
    public class MissionController : WebApiController
    {
        private MissionQueryHandler handler;
        protected ILogger<MissionController> logger;
       
        protected IConfiguration configuration;

        public MissionController(ILogger<MissionController> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            handler = new MissionQueryHandler(logger, configuration);
        }

        /// <summary>Methode GET de recuperation de mission</summary>
        /// <param name="id">Identifiant de la mission a recuperer</param>
        /// <returns>Les informations de la mission</returns>
        [Authorize(Roles="getMission")]
        [HttpGet]
        [Route("/v1/mission/{Id}")]
        [ProducesResponseType(200, Type = typeof(Mission))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetMissionAsync([FromRoute] string id)
        {
            using (Operation.Time("Demande de récupération de données d'une mission")) 
            {
                Mission mission = await handler.GetMissionAsync(id);
                return Ok(mission);
            }
        }

        /// <summary>Methode POST d'ajout de mission</summary>
        /// <param name="mission">Mission a ajouter</param>
        /// <returns>Les informations de la mission</returns>
        [Authorize(Roles="postMission")]
        [HttpPost]
        [Route("/v1/mission")]
        [ProducesResponseType(201, Type = typeof(Mission))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddMissionAsync([FromBody] Mission mission)
        {
            using (Operation.Time("Ajout d'une mission")) 
            {
                mission.MissionState = Mission.MissionStateEnum.CREATED;
                Mission addedMission = await handler.AddMissionAsync(mission);
                return Created(configuration["API:Urls:Mission:Add"], addedMission);
            }
        }

        /// <summary>Methode PUT de mise a jour de mission</summary>
        /// <param name="id">Identifiant de la mission a mettre a jour</param>
        [Authorize(Roles="putMission")]
        [HttpPut]
        [Route("/v1/mission")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateMissionAsync([FromBody] Mission mission)
        {
            Mission addedMission = await handler.AddMissionAsync(mission);
            return Ok(addedMission);
        }

        /// <summary>Methode DELETE de suppression de missions</summary>
        /// <param name="id">Identifiant de la mission a supprimer</param>
        [Authorize(Roles="deleteMission")]
        [HttpDelete]
        [Route("/v1/mission/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteMissionAsync([FromRoute] string id)
        {
            await handler.DeleteMissionAsync(id);
            return Ok();
        }
        /// <summary>Methode GET de recuperation des comptes</summary>
        /// <param name="id">Identifiant de l'utilisateur pour lequel recuperer les comptes</param>
        /// <returns>Les informations du compte de l'utilisateur</returns>
        [Authorize(Roles="listMission")]
        [HttpGet]
        [Route("/v1/mission/account/{id}")]
        [ProducesResponseType(200, Type = typeof(List<Mission>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> ListMissionsAsync([FromRoute] string id, [FromQuery] string accountType)
        {
            SearchMissionCriterias criterias = new SearchMissionCriterias();

            if (string.IsNullOrEmpty(accountType) && !string.IsNullOrEmpty(id))
            {
                criterias.Id = id;
            }

            AccountTypeEnum accountTypeEnum = AccountTypeEnum.ValueOf(accountType);
            if (string.IsNullOrEmpty(accountType))
            {
                throw new ArgumentException("Le type de compte est obligatoire");
            } else if (AccountTypeEnum.AGENCY.Value.Equals(accountType))
            {
                criterias.AgencyAccountId = id;
            } else if (AccountTypeEnum.CANDIDATE.Value.Equals(accountType))
            {
                criterias.CandidateAccountId = id;
            } else if (AccountTypeEnum.COMPANY.Value.Equals(accountType))
            {
                criterias.CompanyAccountId = id;
            }
            List<Mission> missions = await handler.ListMissionAsync(criterias);
            return Ok(new ListMissionsResponse() {
                Missions = missions
            });
        }

    }
}