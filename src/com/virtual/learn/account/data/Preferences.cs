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
    /// parametrages de l&#39;utilisateur
    /// </summary>
    public partial class Preferences
    { 
        /// <summary>
        /// secteurs privilegies
        /// </summary>
        /// <value>secteurs privilegies</value>
        public List<string> DesiredSectors { get; set; }

        /// <summary>
        /// types de contrats privilegies
        /// </summary>
        /// <value>types de contrats privilegies</value>
        public List<string> DesiredContracts { get; set; }

        /// <summary>
        /// type d&#39;horaires privilegies
        /// </summary>
        /// <value>type d&#39;horaires privilegies</value>
        public List<string> ScheduleType { get; set; }

        /// <summary>
        /// metiers privilegies
        /// </summary>
        /// <value>metiers privilegies</value>
        public List<string> DesiredTrades { get; set; }

    }
}
