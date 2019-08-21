using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace compte.Accounts.Collaborator
{
    public class ReferentAccount
    {
        /// <summary>
        /// Identifiant du compte referent
        /// </summary>
        [JsonProperty("userId")]
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Type de compte du compte referent
        /// </summary>
        [JsonProperty("accountType")]
        public string AccountType { get; set; }
    }
}