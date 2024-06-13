namespace AISTN.InternalAppAPI.Models.Filter
{
    public class ReportSerachFilter
    {
        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public string? CourtName { get; set; }

        public Guid? ReportSampleKindId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set;}
    }
}
