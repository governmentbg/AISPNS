using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomCaseCode
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? SetIsStabilizationFlag { get; set; }

    public bool? SetIsProprietorFlag { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
