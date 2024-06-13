using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Sample
{
    public Guid Id { get; set; }

    public Guid SampleKindId { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public Guid? GeneratedDocumentCollectionId { get; set; }

    public Guid? AttachedDocumentCollectionId { get; set; }

    public virtual DocumentCollection? AttachedDocumentCollection { get; set; }

    public virtual DocumentCollection? GeneratedDocumentCollection { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual NomSampleKind SampleKind { get; set; } = null!;
}
