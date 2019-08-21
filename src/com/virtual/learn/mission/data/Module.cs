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
    /// <summary>Module</summary>
    public partial class Module
    { 
        /// <summary>identifiant du module</summary>
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>libellé du module</summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>niveau du module</summary>
        [Required]
        [JsonProperty("level")]
        public int? Level { get; set; }

    }
}
