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
    /// definition d&#39;un module (ou competence) et de son niveau de competence
    /// </summary>
    public partial class Modules
    { 
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Level
        /// </summary>
        public int? Level { get; set; }

    }
}
