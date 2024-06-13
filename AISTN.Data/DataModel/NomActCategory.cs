using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomActCategory
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Act> Acts { get; set; } = new List<Act>();
}
