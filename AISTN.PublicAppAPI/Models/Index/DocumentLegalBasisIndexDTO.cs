namespace AISTN.PublicAppAPI.Models.Index
{
    public class DocumentLegalBasisIndexDTO
    {
        public Guid? Id { get; set; }

        public string? Description { get; set; }

        public bool? IsPublished { get; set; }

        public DateTime? Date { get; set; }

        public Guid? DocumentContentId { get; set; }
    }
}
