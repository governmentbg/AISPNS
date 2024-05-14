using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class CaseAssignment
{
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }

    public bool Active { get; set; }

    public Guid CaseId { get; set; }

    public Guid SyndicId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual Syndic Syndic { get; set; } = null!;
}
