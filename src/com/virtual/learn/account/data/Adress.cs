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
    /// Objet adresse
    /// </summary>
    public partial class Adress
    { 
        /// <summary>
        /// numero de la voie
        /// </summary>
        /// <value>numero de la voie</value>
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// libelle de la voie
        /// </summary>
        /// <value>libelle de la voie</value>
        [Required]
        public string Label { get; set; }

        /// <summary>
        /// Complement n°1
        /// </summary>
        /// <value>Complement n°1</value>
        public string Complement1 { get; set; }

        /// <summary>
        /// Complement n°2
        /// </summary>
        /// <value>Complement n°2</value>
        public string Complement2 { get; set; }

        /// <summary>
        /// Code postal
        /// </summary>
        /// <value>Code postal</value>
        [Required]
        public string PostalCode { get; set; }

        /// <summary>
        /// ville/village
        /// </summary>
        /// <value>ville/village</value>
        [Required]
        public string Town { get; set; }

        /// <summary>
        /// pays
        /// </summary>
        /// <value>pays</value>
        public string Country { get; set; }

    }
}
