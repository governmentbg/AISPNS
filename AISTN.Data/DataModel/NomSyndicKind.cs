using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSyndicKind
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Syndic> Syndics { get; set; } = new List<Syndic>();
}
