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
    /// <summary>Informations sur la caisse de retraite</summary>
    [DataContract]
    public partial class PensionFund
    { 
        /// <summary>Nom de la caisse de retraite complémentaire</summary>
        [JsonProperty("fundName")]
        public string FundName { get; set; }

        /// <summary>Gets or Sets FundAdress</summary>
        [JsonProperty("fundAdress")]
        public Adress FundAdress { get; set; }

    }
}
