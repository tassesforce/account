namespace mission.Models.Search
{ 
    /// <summary>Criteres de recherche des missions</summary>
    public partial class SearchMissionCriterias
    {
        /// <summary> Identifiant de la mission a rechercher</summary>
        public string Id {get; set;}
        /// <summary> Identifiant du compte candidat a rechercher</summary>
        public string CandidateAccountId {get; set;}
        /// <summary> Identifiant du compte agence a rechercher</summary>
        public string AgencyAccountId {get; set;}
        /// <summary> Identifiant du compte entreprise a rechercher</summary>
        public string CompanyAccountId {get; set;}
    }
}