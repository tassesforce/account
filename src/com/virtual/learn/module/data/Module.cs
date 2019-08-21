using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace compte.Modules
{
    /// <summary>Represent a module</summary>
    [BsonIgnoreExtraElements]
    public class Module
    {
        /// <summary>Type of the module (hygiene, security, etc.)</summary>
        [BsonElement("type")]
        public string Type {get; set;}
        
        /// <summary>Approximative length of the module in minutes</summary>
        [BsonElement("length")]
        public int Length {get; set;}
        /// <summary>Number of credits needed to pass the module</summary>
        [BsonElement("nbCredits")]
        public int NbCredits {get; set;}
        /// <summary>Label of the module</summary>
        [BsonElement("label")]
        public string Label {get; set;}
        /// <summary>Unique identifier of the module</summary>
        [BsonElement("moduleId")]
        public string ModuleId {get; set;}
        /// <summary>Unique identifier of the account owning the module</summary>
        [BsonElement("userId")]
        public string UserId {get; set;}

        /// <summary>URL where to find the media</summary>
        [BsonElement("media")]
        public string Media {get; set;}
        
    }
}