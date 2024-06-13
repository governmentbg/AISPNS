using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid? OrderKindId { get; set; }

    public string? Number { get; set; }

    public DateTime? Date { get; set; }

    public string? StateGazetteEdition { get; set; }

    public string? StateGazetteYear { get; set; }

    public string? Description { get; set; }

    public Guid SyndicId { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public Guid? OrderPaymentKindId { get; set; }

    public string? ExclusionGrounds { get; set; }

    public DateTime? ExclusionStartDate { get; set; }

    public DateTime? ExclusionEndDate { get; set; }

    public DateTime? ExclusionTemporaryDate { get; set; }

    public bool IsExclusion { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<Appeal> Appeals { get; set; } = new List<Appeal>();

    public virtual NomOrderKind? OrderKind { get; set; }

    public virtual NomOrderPaymentKind? OrderPaymentKind { get; set; }

    public virtual Syndic Syndic { get; set; } = null!;
}
