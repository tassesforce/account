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
    /// definition du parcours
    /// </summary>
    public partial class Course
    { 
        /// <summary>
        /// formation scolaire
        /// </summary>
        /// <value>formation scolaire</value>
        public List<string> Scolar { get; set; }

        /// <summary>
        /// experiences professionnelles
        /// </summary>
        /// <value>experiences professionnelles</value>
        public List<string> Experiences { get; set; }

        /// <summary>
        /// missions effectuees
        /// </summary>
        /// <value>missions effectuees</value>
        public List<string> Missions { get; set; }

        /// <summary>
        /// reussites obtenues
        /// </summary>
        /// <value>reussites obtenues</value>
        public List<string> Successes { get; set; }

    }
}
