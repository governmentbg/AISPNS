namespace AISTN.Common.Models
{
    public class NomAttachedDocumentKindDTO
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastModified { get; set; }

        public int? ViewOrder { get; set; }
    }
}
