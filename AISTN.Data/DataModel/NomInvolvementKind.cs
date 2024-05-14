using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomInvolvementKind
{
    public Guid Id { get; set; }

    public int Code { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Side> Sides { get; set; } = new List<Side>();
}
