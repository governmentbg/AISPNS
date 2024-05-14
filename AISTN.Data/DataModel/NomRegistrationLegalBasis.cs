using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomRegistrationLegalBasis
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public string? Subject { get; set; }

    public virtual ICollection<RegisterEntry> RegisterEntries { get; set; } = new List<RegisterEntry>();
}
