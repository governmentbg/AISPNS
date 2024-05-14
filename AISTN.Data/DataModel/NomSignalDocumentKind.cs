using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSignalDocumentKind
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<DocumentContent> DocumentContents { get; set; } = new List<DocumentContent>();
}
