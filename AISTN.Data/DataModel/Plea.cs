using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Plea
{
    public Guid Id { get; set; }

    public string? OrderNumber { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? PleaNumber { get; set; }

    public DateTime? PleaDate { get; set; }

    public string? CourtDecision { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public Guid SyndicId { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual Syndic Syndic { get; set; } = null!;
}
