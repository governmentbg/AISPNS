using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomCompanySize
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<EntityStatisticalDatum> EntityStatisticalData { get; set; } = new List<EntityStatisticalDatum>();
}
