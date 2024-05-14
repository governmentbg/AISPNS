using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomActReason
{
    public Guid Id { get; set; }

    public int Code { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Act> Acts { get; set; } = new List<Act>();
}
