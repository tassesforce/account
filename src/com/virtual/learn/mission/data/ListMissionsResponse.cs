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
    /// <summary>liste de missions</summary>
    public partial class ListMissionsResponse
    { 

        /// <summary>Liste des missions</summary>
        [JsonProperty("missions")]
        public List<Mission> Missions { get; set; }

    }
}
