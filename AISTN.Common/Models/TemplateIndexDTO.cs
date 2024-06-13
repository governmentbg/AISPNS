namespace AISTN.Common.Models
{
    public class TemplateIndexDTO
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public string? TemplateKindName { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public Guid? DocumentContentID { get; set; }
    }
}
