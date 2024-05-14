using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomActivityKind
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
