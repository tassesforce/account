using Newtonsoft.Json;

namespace compte.Accounts.List
{
    public class AccountFiliation
    {
        [JsonProperty("idEnfant")]
        public string IdEnfant {get; set;}
        
        [JsonProperty("idParent")]
        public string IdParent {get; set;}
        [JsonProperty("status")]
        public string Status {get; set;}
    }
}