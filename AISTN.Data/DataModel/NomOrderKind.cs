using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomOrderKind
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
