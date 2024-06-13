using Microsoft.AspNetCore.Http;

namespace AISTN.Common.Models
{
    public class MessageAttachmentDTO
    {
        public Guid? Id { get; set; }

        public Guid? MessageId { get; set; }

        public IFormFile File { get; set; }

        public int? DocumentKind { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public string? Note { get; set; }

        public Guid? DocumentCollectionId { get; set; }
    }
}
