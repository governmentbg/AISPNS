namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveAdminTemplateDTO
    {
        public Guid? Id { get; set; }

        public string? TemplateName { get; set; }

        public DateTime? Date { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public Guid? TemplateKindId { get; set; }
    }
}
