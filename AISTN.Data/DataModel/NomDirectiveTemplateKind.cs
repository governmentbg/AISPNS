using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomDirectiveTemplateKind
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TemplateArticle28> TemplateArticle28s { get; set; } = new List<TemplateArticle28>();
}
