using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace mission.Models
{ 
    /// <summary>Definition du poste</summary>
    public partial class Job
    { 
        /// <summary>nombre de postes à pourvoir</summary>
        [Required]
        [JsonProperty("numberOfJobs")]
        public int? NumberOfJobs { get; set; }

        /// <summary>Date de début de la mission</summary>
        [Required]
        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        /// <summary>Date de fin de la mission</summary>
        [Required]
        [JsonProperty("endDate")]
        public string EndDate { get; set; }

        /// <summary>libellé du poste</summary>
        [Required]
        [JsonProperty("jobLabel")]
        public string JobLabel { get; set; }

        /// <summary>description du poste</summary>
        [JsonProperty("jobDescription")]
        public string JobDescription { get; set; }

        /// <summary>Gets or Sets Skills</summary>
        [JsonProperty("skills")]
        public List<Skill> Skills { get; set; }

        /// <summary>Gets or Sets Modules</summary>
        [JsonProperty("modules")]
        public List<Module> Modules { get; set; }

        /// <summary>Poste à risque</summary>
        [JsonProperty("riskyJob")]
        public bool RiskyJob { get; set; }

    }
}
