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
    /// <summary>Commentaire de satisfaction sur la mission</summary>
    public partial class Satisfaction
    { 
        /// <summary>identifiant du compte laissant le commentaire</summary>
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>identifiant du compte sur lequel porte le commentaire (entreprise notant la prise en charge de la mission par l'agence ou la qualité de prestation du candidat)</summary>
        [Required]
        [JsonProperty("idTarget")]
        public string IdTarget { get; set; }

        /// <summary>commentaire</summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>note a laisser pour le compte cible (de 0 à 10)</summary>
        [Required]
        [JsonProperty("score")]
        public int? Score { get; set; }

    }
}
