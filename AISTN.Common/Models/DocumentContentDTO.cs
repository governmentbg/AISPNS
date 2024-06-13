namespace AISTN.Common.Models
{
    public class DocumentContentDTO
    {
        public Guid Id { get; set; }

        public Guid? BlobId { get; set; }

        public string? ContentMimeType { get; set; }

        public double? FileSize { get; set; }

        public string? FileName { get; set; }

        public DateTime? DocumentDate { get; set; }

        public DateTime? BlobDateCreated { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public Guid? AttachedDocumentKindId { get; set; }

        public Guid? SignalDocumentKindId { get; set; }

        public NomAttachedDocumentKindDTO? AttachedDocumentKind { get; set; }

        public NomSignalDocumentKindDTO? SignalDocumentKind { get; set; }
    }
}
