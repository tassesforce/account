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
    /// partenaires, liste des identifiants de partenaires, des favoris et des blackliste
    /// </summary>
    public partial class Partners
    { 
        /// <summary>
        /// Gets or Sets Ids
        /// </summary>
        public List<string> Ids { get; set; }

        /// <summary>
        /// Gets or Sets Blacklist
        /// </summary>
        public List<string> Blacklist { get; set; }

        /// <summary>
        /// Gets or Sets Favorites
        /// </summary>
        public List<string> Favorites { get; set; }

    }
}
