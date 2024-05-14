using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Entity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Bulstat { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<EntityStatisticalDatum> EntityStatisticalData { get; set; } = new List<EntityStatisticalDatum>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    public virtual ICollection<Side> Sides { get; set; } = new List<Side>();
}
