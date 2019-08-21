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
    /// definition des capacites de deplacement
    /// </summary>
    public partial class Mobility
    { 
        /// <summary>
        /// accepte de se deplacer
        /// </summary>
        /// <value>accepte de se deplacer</value>
        [Required]
        public bool? Accepted { get; set; }

        /// <summary>
        /// moyen de locomotion
        /// </summary>
        /// <value>moyen de locomotion</value>
        public string Transport { get; set; }

        /// <summary>
        /// type de moyen de locomotion
        /// </summary>
        /// <value>type de moyen de locomotion</value>
        public string TransportType { get; set; }

        /// <summary>
        /// rayon de deplacement
        /// </summary>
        /// <value>rayon de deplacement</value>
        public string TransportArea { get; set; }

    }
}
