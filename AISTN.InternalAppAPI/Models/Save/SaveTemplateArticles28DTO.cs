namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveTemplateArticles28DTO
    {
        public Guid? Id { get; set; }

        public string? TemplateName { get; set; }

        public DateTime? Date { get; set; }

        public Guid? DirectiveTemplateKindId { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public bool IsPublished { get; set; }
    }
}
