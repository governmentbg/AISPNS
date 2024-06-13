using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomAttachedDocumentKind
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? LastModified { get; set; }

    public int? ViewOrder { get; set; }

    public virtual ICollection<DocumentContent> DocumentContents { get; set; } = new List<DocumentContent>();
}
