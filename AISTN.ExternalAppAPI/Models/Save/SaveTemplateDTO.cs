namespace AISTN.ExternalAppAPI.Models.Save
{
    public class SaveTemplateDTO
    {
        public Guid? Id { get; set; }

        public Guid? SyndicId { get; set; }

        public Guid? TemplateKindId { get; set; }

        public string? Description { get; set; }

        public DateTime? Date { get; set; }

        public Guid? DocumentCollectionId { get; set; }
    }
}
