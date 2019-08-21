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
    /// caisse de retraite a laquelle souscrit l&#39;entreprise
    /// </summary>
    public partial class Fund
    { 
        /// <summary>
        /// nom de la caisse
        /// </summary>
        /// <value>nom de la caisse</value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Adress
        /// </summary>
        [Required]
        public Adress Adress { get; set; }

    }
}
