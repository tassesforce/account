using Newtonsoft.Json;

namespace compte.Modules
{
    /// <summary>Criterias to search a list of modules</summary>
    public class ListModulesRequest
    {
        /// <summary>Type of the module (hygiene, security, etc.)</summary>
        [JsonProperty("type")]
        public string Type {get; set;}
        
        /// <summary>Approximative length of the module in minutes</summary>
        [JsonProperty("length")]
        public int? Length {get; set;}
        /// <summary>Number of credits needed to pass the module</summary>
        [JsonProperty("ndCredits")]
        public int? NbCredits {get; set;}
        /// <summary>Label of the module</summary>
        [JsonProperty("label")]
        public string Label {get; set;}
        /// <summary>Unique identifier of the module</summary>
        [JsonProperty("moduleId")]
        public string ModuleId {get; set;}
        /// <summary>Unique identifier of the account owning the module</summary>
        [JsonProperty("userId")]
        public string UserId {get; set;}
        
    }
}