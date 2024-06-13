using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomAppealKind
{
    public Guid Id { get; set; }

    public int Code { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Appeal> Appeals { get; set; } = new List<Appeal>();
}
