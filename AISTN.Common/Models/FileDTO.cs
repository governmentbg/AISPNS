using Microsoft.AspNetCore.Http;

namespace AISTN.Common.Models
{
    public class FileDTO
    {
        public Guid Id { get; set; }
        public IFormFile File { get; set; }
        public Guid? DocumentContentType { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public DateTime? DocumentDate { get; set; }
        public Guid? AttachedDocumentKindId { get; set; }
        public Guid? AttachedDocumentCollectionId { get; set; }
        public Guid? SignalDocumentKindId { get; set; }
    }
}
