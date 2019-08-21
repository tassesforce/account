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
    /// <summary>Information sur la personne a contacter</summary>
    public partial class Contact
    { 
        /// <summary>Prénom du contact</summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>Nom de famille du contact</summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>Numéro de téléphone de la personne à contacter</summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>Adresse mail de la personne à contacter</summary>
        [JsonProperty("mailAdress")]
        [Required]
        public string MailAdress { get; set; }

    }
}
