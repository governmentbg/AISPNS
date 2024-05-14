using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Experience
{
    public Guid Id { get; set; }

    public Guid CaseId { get; set; }

    public Guid SyndicId { get; set; }

    public Guid? DebtorId { get; set; }

    public int? Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? AssginedDate { get; set; }

    public string? RemovalGrounds { get; set; }

    public string? Notes { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual Syndic Syndic { get; set; } = null!;
}
