using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomCaseKind
{
    public Guid Id { get; set; }

    public int? Code { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
