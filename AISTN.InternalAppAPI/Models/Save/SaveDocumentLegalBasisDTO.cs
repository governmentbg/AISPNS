using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveDocumentLegalBasisDTO
    {
        public Guid? Id { get; set; }

        public string? Description { get; set; }

        public bool? IsPublished { get; set; }

        public DateTime? Date { get; set; }

        public Guid? DocumentCollectionId { get; set; }
    }
}
