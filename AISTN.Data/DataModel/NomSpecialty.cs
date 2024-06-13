using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSpecialty
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Syndic> Syndics { get; set; } = new List<Syndic>();
}
