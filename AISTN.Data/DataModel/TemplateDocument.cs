using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class TemplateDocument
{
    public Guid Id { get; set; }

    public Guid? BlobId { get; set; }

    /// <summary>
    /// Тип на файл
    /// </summary>
    public string? ContentMimeType { get; set; }

    /// <summary>
    /// Размер на файл
    /// </summary>
    public double? FileSize { get; set; }

    /// <summary>
    /// Име на файл
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Дата на документ
    /// </summary>
    public DateTime? DocumentDate { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    public string? Notes { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public string? Type { get; set; }

    public virtual Blob? Blob { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }
}
