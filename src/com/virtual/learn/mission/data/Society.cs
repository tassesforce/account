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
    /// <summary>Informations sur la société prescriptive de la mission</summary>
    public partial class Society
    { 
        ///<summary>Identifiant du compte entreprise lie</summary>
        public string CompanyId { get; set; }
        /// <summary>Raison sociale de l&#39;entreprise</summary>
        [Required]
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>Gets or Sets Adress</summary>
        [Required]
        [JsonProperty("Adress")]
        public Adress Adress { get; set; }

        /// <summary>Gets or Sets Responsible</summary>
        [JsonProperty("Responsible")]
        public Contact Responsible { get; set; }

    }
}
