namespace AISTN.InternalAppAPI.Models.Save;

public class SaveTemplateDocumentDTO
{
    public Guid? Id { get; set; }

    public string? ContentMimeType { get; set; }

    public double? FileSize { get; set; }

    public string? FileName { get; set; }

    public DateTime? DocumentDate { get; set; }

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public string? Type { get; set; }

    public Guid? DocumentCollectionId { get; set; }
}