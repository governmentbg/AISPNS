using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomPropertyClass
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
