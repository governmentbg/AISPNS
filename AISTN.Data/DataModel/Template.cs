using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Template
{
    public Guid Id { get; set; }

    public Guid? TemplateKindId { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;

    public Guid? SyndicId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual Syndic? Syndic { get; set; }

    public virtual NomTemplateKind? TemplateKind { get; set; }
}
