using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Blob
{
    public Guid Id { get; set; }

    public byte[]? DocumentContent { get; set; }

    public DateTime? DateCreated { get; set; }

    public virtual ICollection<DocumentContent> DocumentContents { get; set; } = new List<DocumentContent>();

    public virtual ICollection<TemplateDocument> TemplateDocuments { get; set; } = new List<TemplateDocument>();
}
