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

namespace compte.Models.POC
{ 
    /// <summary>
    /// Objet adresse
    /// </summary>
    public partial class DatasPOC
    { 

        [JsonProperty("module")]
        [Required]
        public string Module { get; set; }

        [JsonProperty("datas")]
        [Required]
        public string Datas { get; set; }

        public override string ToString()
        {
            return "module : " + Module + ", datas : " + Datas;
        }

    }
}
