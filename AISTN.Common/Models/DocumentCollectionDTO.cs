namespace AISTN.Common.Models
{
    public class DocumentCollectionDTO
    {
        public Guid? Id { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public IEnumerable<DocumentContentDTO>? DocumentContents { get; set; }
    }
}
