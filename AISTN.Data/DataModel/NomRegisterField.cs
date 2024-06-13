using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomRegisterField
{
    public Guid Id { get; set; }

    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? IsStabilization { get; set; }

    public virtual ICollection<RegisterEntry> RegisterEntries { get; set; } = new List<RegisterEntry>();
}
