namespace AISTN.ExternalAppAPI.Models.Filter
{
    public class ReportSearchFilter
    {
        public string? CaseNumber { get; set; }

        public int? CaseYear { get; set; }

        public int? CourtNumber { get; set; }

        public string? SyndicFirstName { get; set; }

        public string? SyndicLastName { get; set; }

        public string? DebtorName { get; set; }

        public string? DebtorIdentifier { get; set; }
    }
}
