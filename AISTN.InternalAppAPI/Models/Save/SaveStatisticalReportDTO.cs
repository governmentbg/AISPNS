namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveStatisticalReportDTO
    {
        public Guid? Id { get; set; }

        public string? IdentificationNumbr { get; set; }

        public Guid? Type { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public Guid? ReportSourceId { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public string? Note { get; set; }

        public DateTime? Published { get; set; }

        public bool? IsPublished { get; set; }
    }
}
