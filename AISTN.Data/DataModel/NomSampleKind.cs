using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSampleKind
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Sample> Samples { get; set; } = new List<Sample>();
}
