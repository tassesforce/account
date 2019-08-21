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
    /// <summary>Compétence</summary>
    public partial class Skill
    { 
        /// <summary>identifiant de la compétence</summary>
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>libellé de la compétence</summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>niveau de la compétence</summary>
        [Required]
        [JsonProperty("level")]
        public int? Level { get; set; }

    }
}
