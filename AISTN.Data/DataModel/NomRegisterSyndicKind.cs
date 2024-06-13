using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomRegisterSyndicKind
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<RegisterEntry> RegisterEntries { get; set; } = new List<RegisterEntry>();
}
