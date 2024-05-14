using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomInspectionType
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
}
