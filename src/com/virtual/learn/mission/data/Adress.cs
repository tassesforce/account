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
    /// <summary>Objet adresse</summary>
    public partial class Adress
    { 
        /// <summary>numero de la voie</summary>
        [Required]
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>libelle de la voie</summary>
        [Required]
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>Complement n°1</summary>
        [JsonProperty("complement1")]
        public string Complement1 { get; set; }

        /// <summary>Complement n°2</summary>
        [JsonProperty("complement2")]
        public string Complement2 { get; set; }

        /// <summary>Code postal</summary>
        [Required]
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>ville/village</summary>
        [Required]
        [JsonProperty("town")]
        public string Town { get; set; }

        /// <summary>pays</summary>
        [JsonProperty("country")]
        public string Country { get; set; }

    }
}
