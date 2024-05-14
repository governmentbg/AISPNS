namespace AISTN.ExternalAppAPI.Models.Filter
{
    public class PropertySearchFilter
    {
        public string? CaseNumber { get; set; }

        public int? CaseYear { get; set; }

        public string? CourtName { get; set; }

        public string? DebtorName { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
