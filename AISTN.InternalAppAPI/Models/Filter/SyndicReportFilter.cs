namespace AISTN.InternalAppAPI.Models.Filter
{
    public class SyndicReportFilter
    {
        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public int? CourtNumber { get; set; }

        public Guid? ReportTypeId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
