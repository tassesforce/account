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
using MongoDB.Bson.Serialization.Attributes;

namespace mission.Models
{ 
    /// <summary>Ressource pour une mission</summary>
    [BsonIgnoreExtraElements]
    public partial class Mission
    { 
        /// <summary>identifiant de la mission</summary>
        [JsonProperty("missionId")]
        public string MissionId { get; set; }

        /// <summary>Gets or Sets Society</summary>
        [Required]
        [JsonProperty("society")]
        public Society Society { get; set; }

        /// <summary>Gets or Sets Contact</summary>
        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        /// <summary>secteur d'activité de la mission</summary>
        [Required]
        [JsonProperty("sector")]
        public string Sector { get; set; }

        /// <summary>Date de publication de la mission</summary>
        [Required]
        [JsonProperty("publishingDate")]
        public string PublishingDate { get; set; }

        /// <summary>Gets or Sets Job</summary>
        [Required]
        [JsonProperty("job")]
        public Job Job { get; set; }

        /// <summary>Type de contrat</summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum ContractTypeEnum
        {
            
            /// <summary>
            /// Enum CDI for CDI
            /// </summary>
            [EnumMember(Value = "CDI")]
            CDI = 1,
            
            /// <summary>
            /// Enum CDD for CDD
            /// </summary>
            [EnumMember(Value = "CDD")]
            CDD = 2,
            
            /// <summary>
            /// Enum Interim for Interim
            /// </summary>
            [EnumMember(Value = "Interim")]
            Interim = 3
        }

        /// <summary>Type de contrat</summary>
        [Required]
        [JsonProperty("contractType")]
        public ContractTypeEnum? ContractType { get; set; }

        /// <summary>Niveau scolaire requis (diplome, etc.)</summary>
        [JsonProperty("schoolLevel")]
        public string SchoolLevel { get; set; }

        /// <summary>Expérience requise</summary>
        [JsonProperty("experience")]
        public string Experience { get; set; }

        /// <summary>Salaire de la mission</summary>
        [Required]
        [JsonProperty("salary")]
        public int Salary { get; set; }

        /// <summary>Type de salaire</summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum SalaryTypeEnum
        {
            
            /// <summary>
            /// Enum HORAIRE for HORAIRE
            /// </summary>
            [EnumMember(Value = "HORAIRE")]
            HORAIRE = 1,
            
            /// <summary>
            /// Enum QUOTIDIEN for QUOTIDIEN
            /// </summary>
            [EnumMember(Value = "QUOTIDIEN")]
            QUOTIDIEN = 2,
            
            /// <summary>
            /// Enum HEBDOMADAIRE for HEBDOMADAIRE
            /// </summary>
            [EnumMember(Value = "HEBDOMADAIRE")]
            HEBDOMADAIRE = 3,
            
            /// <summary>
            /// Enum MENSUEL for MENSUEL
            /// </summary>
            [EnumMember(Value = "MENSUEL")]
            MENSUEL = 4,
            
            /// <summary>
            /// Enum ANNUEL for ANNUEL
            /// </summary>
            [EnumMember(Value = "ANNUEL")]
            ANNUEL = 5
        }

        /// <summary>Type de salaire</summary>
        [JsonProperty("salaryType")]
        public SalaryTypeEnum? SalaryType { get; set; }

        /// <summary>horaires de la mission</summary>
        [Required]
        [JsonProperty("schedule")]
        public string Schedule { get; set; }

        /// <summary>Type d'horaires de la mission</summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum MissionTypeEnum
        {
            
            /// <summary>
            /// Enum JOURNEE for JOURNEE
            /// </summary>
            [EnumMember(Value = "JOURNEE")]
            JOURNEE = 1,
            
            /// <summary>
            /// Enum _28 for 2*8
            /// </summary>
            [EnumMember(Value = "2*8")]
            _28 = 2,
            
            /// <summary>
            /// Enum _38 for 3*8
            /// </summary>
            [EnumMember(Value = "3*8")]
            _38 = 3,
            
            /// <summary>
            /// Enum NUIT for NUIT
            /// </summary>
            [EnumMember(Value = "NUIT")]
            NUIT = 4,
            
            /// <summary>
            /// Enum MATIN for MATIN
            /// </summary>
            [EnumMember(Value = "MATIN")]
            MATIN = 5
        }

        /// <summary>Type d'horaires de la mission</summary>
        [JsonProperty("missionType")]
        public MissionTypeEnum? MissionType { get; set; }

        /// <summary>Type d'emploi</summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum JobTypeEnum
        {
            
            /// <summary>
            /// Enum PLEIN for TEMPS PLEIN
            /// </summary>
            [EnumMember(Value = "TEMPS PLEIN")]
            PLEIN = 1,
            
            /// <summary>
            /// Enum PARTIEL for TEMPS PARTIEL
            /// </summary>
            [EnumMember(Value = "TEMPS PARTIEL")]
            PARTIEL = 2
        }

        /// <summary>Type d'emploi</summary>
        [JsonProperty("jobType")]
        public JobTypeEnum? JobType { get; set; }

        /// <summary>caractéristiques spécifiques de la mission</summary>
        [JsonProperty("characteristics")]
        public string Characteristics { get; set; }

        /// <summary>Moyen de transport nécessaire</summary>
        [JsonProperty("mandatoryTransport")]
        public bool? MandatoryTransport { get; set; }

        /// <summary>Statut de la mission</summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum MissionStateEnum
        {
            
            /// <summary>
            /// Enum CREATED for CREATED
            /// </summary>
            [EnumMember(Value = "CREATED")]
            CREATED = 1,
            
            /// <summary>
            /// Enum INPROGRESS for IN_PROGRESS
            /// </summary>
            [EnumMember(Value = "IN_PROGRESS")]
            INPROGRESS = 2,
            
            /// <summary>
            /// Enum STOPPED for STOPPED
            /// </summary>
            [EnumMember(Value = "STOPPED")]
            STOPPED = 3,
            
            /// <summary>
            /// Enum TERMINATED for TERMINATED
            /// </summary>
            [EnumMember(Value = "TERMINATED")]
            TERMINATED = 4
        }

        /// <summary>Statut de la mission</summary>
        [JsonProperty("missionState")]
        public MissionStateEnum? MissionState { get; set; }

        /// <summary>Période d'essai</summary>
        [Required]
        [JsonProperty("trialPeriod")]
        public bool? TrialPeriod { get; set; }

        /// <summary>Durée de la période d'essai</summary>
        [JsonProperty("durationTrialPeriod")]
        public string DurationTrialPeriod { get; set; }

        /// <summary>Motivation de la mission</summary>
        [Required]
        [JsonProperty("missionReason")]
        public string MissionReason { get; set; }

        /// <summary>Clause d'aménagement</summary>
        [Required]
        [JsonProperty("layoutClause")]
        public string LayoutClause { get; set; }

        /// <summary>Gets or Sets PensionFund</summary>
        [Required]
        [JsonProperty("pensionFund")]
        public PensionFund PensionFund { get; set; }

        /// <summary>Clause de rapatriement (cas hors France métropolitaine)</summary>
        [Required]
        [JsonProperty("repatriationClause")]
        public string RepatriationClause { get; set; }

        /// <summary>liste des candidats envoyés pour la mission</summary>
        [JsonProperty("candidates")]
        public List<string> Candidates { get; set; }

        /// <summary>Agence responsable de la mission</summary>
        [JsonProperty("agency")]
        public string Agency { get; set; }

        /// <summary>Possibilité laissée aux acteurs de la mission de laisser des commentaires de satisfaction sur la mission</summary>
        [JsonProperty("satisfactions")]
        public List<Satisfaction> Satisfactions { get; set; }

    }
}
