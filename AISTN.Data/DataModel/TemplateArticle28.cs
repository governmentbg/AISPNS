using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class TemplateArticle28
{
    public Guid Id { get; set; }

    public string? TemplateName { get; set; }

    public DateTime? Date { get; set; }

    public Guid? DirectiveTemplateKindId { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public bool IsPublished { get; set; }

    public virtual NomDirectiveTemplateKind? DirectiveTemplateKind { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }
}
