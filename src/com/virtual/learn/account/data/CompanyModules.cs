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
    /// descriptions des modules detenues et souahites par l&#39;entreprise
    /// </summary>
    public partial class CompanyModules
    { 
        /// <summary>
        /// liste des modules dedies detenus par l&#39;entreprise
        /// </summary>
        /// <value>liste des modules dedies detenus par l&#39;entreprise</value>
        public List<string> Dedicated { get; set; }

        /// <summary>
        /// liste des modules et competences obligatoires pour postuler dans l&#39;entreprise
        /// </summary>
        /// <value>liste des modules et competences obligatoires pour postuler dans l&#39;entreprise</value>
        public List<Modules> Mandatory { get; set; }

    }
}
