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

namespace compte.Models.Accounts
{ 
    /// <summary>
    /// definition du handicap
    /// </summary>
    public partial class Disability
    { 
        /// <summary>
        /// status handicape ou non
        /// </summary>
        /// <value>status handicape ou non</value>
        public bool? DisableStatus { get; set; }

        /// <summary>
        /// details sur le statut handicape
        /// </summary>
        /// <value>details sur le statut handicape</value>
        public string DisabilityDetails { get; set; }

    }
}
