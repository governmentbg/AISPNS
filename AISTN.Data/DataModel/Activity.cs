using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Activity
{
    public Guid Id { get; set; }

    public Guid ActivityKindId { get; set; }

    public DateTime ActivityDate { get; set; }

    public string? SerialNumber { get; set; }

    public string Description { get; set; } = null!;

    public string? Notes { get; set; }

    public Guid CaseId { get; set; }

    public Guid SyndicId { get; set; }

    public Guid? ReportId { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual NomActivityKind ActivityKind { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual Report? Report { get; set; }

    public virtual Syndic Syndic { get; set; } = null!;
}
