namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveReportTemplateDTO
    {
        public Guid? Id { get; set; }

        public string? TemplateName { get; set; }

        public DateTime? Date { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public Guid? ReportTypeId { get; set; }
    }
}