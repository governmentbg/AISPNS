using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSyndicAction
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
