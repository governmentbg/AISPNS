namespace AISTN.ExternalAppAPI.Models.Filter
{
    public class CaseSyndicSearchFilter
    {
        public int? CourtNumber { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public string? EntityName { get; set; }

        public string? Bulstat { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? EGN { get; set; }

        public Guid? SyndicId { get; set; }
    }
}
