using AISTN.Data.DataModel;

namespace AISTN.PublicAppAPI.Models.Index
{
    public class IndexTemplateArticles28DTO
    {
        public Guid? Id { get; set; }

        public string? TemplateName { get; set; }

        public DateTime? Date { get; set; }

        public string? DirectiveTemplateKindName { get; set; }

        public Guid? DocumentContentId { get; set; }

        public bool IsPublished { get; set; }
    }
}
