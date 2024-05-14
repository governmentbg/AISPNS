using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class AdminTemplate
{
    public Guid Id { get; set; }

    public string? TemplateName { get; set; }

    public DateTime? Date { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public Guid? TemplateKindId { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual NomTemplateKind? TemplateKind { get; set; }
}
