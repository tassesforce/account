using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace cairn.Models.Auth
{
    public class AuthenticateUser
    {
        [JsonIgnore]
        [Required]
        public string Login {get; set;}
        [JsonProperty("password")]
        [Required]
        public string Password {get; set;}
        [JsonProperty("client_id")]
        [Required]
        public string ClientId {get;set;}
        [JsonProperty("client_secret")]
        [Required]
        public string ClientSecret {get;set;}
        [JsonProperty("grant_type")]
        [Required]
        public string GrantType {get;set;}
    }
}