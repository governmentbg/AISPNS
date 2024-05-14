namespace AISTN.InternalAppAPI.Models.Index
{
    public class ReportTemplateIndexDTO
    {
        public Guid? Id { get; set; }

        public string? TemplateName { get; set; }

        public string? ReportType { get; set; }

        public DateTime? Date { get; set; }

        public Guid? DocumentContentID { get; set; }
    }
}