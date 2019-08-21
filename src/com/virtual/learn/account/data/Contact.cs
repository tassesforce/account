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
    /// definition des methodes de contact
    /// </summary>
    public partial class Contact
    { 
        /// <summary>
        /// Numero de telephone
        /// </summary>
        /// <value>Numero de telephone</value>
        [JsonProperty("phone")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// adresse mail
        /// </summary>
        /// <value>adresse mail</value>
        [JsonProperty("mail")]
        [Required]
        public string MailAdress { get; set; }

        /// <summary>
        /// site internet
        /// </summary>
        /// <value>site internet</value>
        [JsonProperty("web")]
        public string WebPage { get; set; }

        /// <summary>
        /// Moyen de contact privilegie
        /// </summary>
        /// <value>Moyen de contact privilegie</value>
        public PreferredContactEnum PreferredContact { get; set; }

    }
}
