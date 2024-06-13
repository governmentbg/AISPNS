namespace AISTN.InternalAppAPI.Models.Index
{
    public class AdminTemplateIndexDTO
    {
        public Guid? Id { get; set; }

        public string? TemplateName { get; set; }

        public string? TemplateKind { get; set; }

        public DateTime? Date { get; set; }

        public Guid? DocumentContentID { get; set; }
    }
}
