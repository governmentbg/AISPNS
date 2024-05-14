namespace AISTN.InternalAppAPI.Models.Filter
{
    public class ReportSampleFilterDTO
    {
        public Guid? ReportSampleKindId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
