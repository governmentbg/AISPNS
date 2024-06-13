using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Trustee
{
    public Guid Id { get; set; }

    public DateTime ImportDate { get; set; }

    public Guid DeedId { get; set; }

    public string? Name { get; set; }

    public string? IndentType { get; set; }

    public string? Indent { get; set; }

    public string? Status { get; set; }

    public string? CountryName { get; set; }

    public Guid? SyndicId { get; set; }

    public string? RecordIncomingNumber { get; set; }

    public int? RecordId { get; set; }

    public DateTime? InductionDate { get; set; }

    public DateTime? AcquittanseDate { get; set; }

    public virtual ImportedDeed Deed { get; set; } = null!;

    public virtual Syndic? Syndic { get; set; }
}
