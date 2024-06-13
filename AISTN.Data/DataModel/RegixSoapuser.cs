using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class RegixSoapuser
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string? HashedPassword { get; set; }

    public string? Salt { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<RegixSoapsession> RegixSoapsessions { get; set; } = new List<RegixSoapsession>();
}
