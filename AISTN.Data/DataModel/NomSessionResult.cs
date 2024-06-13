using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSessionResult
{
    public Guid Id { get; set; }

    public int Code { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
