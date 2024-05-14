namespace AISTN.InternalAppAPI.Models.Filter
{
    public class AnnouncementSearchFilter
    {
        public string? CaseNumber { get; set; }
        public short? CaseYear { get; set; }
        public int? CourtNumber { get; set; }
        public string? DebtorIdentifier { get; set; }
        public string? DebtorName { get; set; }
        public string? SyndicFirstName { get; set; }
        public string? SyndicLastName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? StatusCode { get; set; }
    }
}
