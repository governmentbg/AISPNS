namespace AISTN.ExternalAppAPI.Models.Save
{
    public class SaveReportDTO
    {
        public Guid? Id { get; set; }

        public Guid? ReportTypeId { get; set; }

        public DateTime ReportDate { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string? Notes { get; set; }

        public Guid SyndicId { get; set; }

        public string? SyndicName { get; set; }

        public Guid CaseId { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public Guid? SampleID { get; set;}
    }
}
