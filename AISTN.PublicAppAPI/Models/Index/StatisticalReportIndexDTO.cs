using AISTN.Common.Models;

namespace AISTN.PublicAppAPI.Models.Index
{
    public class StatisticalReportIndexDTO
    {
        public Guid Id { get; set; }

        public DateTime? PublishedDate { get; set; }

        public string? Type { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string? ReportSource { get; set; }
    }
}
