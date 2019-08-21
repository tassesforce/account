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
    /// responsable du compte
    /// </summary>
    public class Responsible
    { 
        /// <summary>
        /// prenom du responsable
        /// </summary>
        /// <value>prenom du responsable</value>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// nom de famille du responsable
        /// </summary>
        /// <value>nom de famille du responsable</value>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// position du responsable dans l&#39;agence
        /// </summary>
        /// <value>position du responsable dans l&#39;agence</value>
        public string Position { get; set; }

    }
}
