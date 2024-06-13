namespace AISTN.PublicAppAPI.Models.Filters
{
    public class StatisticalReportSearchFilter
    {
        public Guid? Type { get; set; }

        public Guid? Source { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
