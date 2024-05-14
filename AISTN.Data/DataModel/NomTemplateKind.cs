using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomTemplateKind
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<AdminTemplate> AdminTemplates { get; set; } = new List<AdminTemplate>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
}
