using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Appeal
{
    public Guid Id { get; set; }

    public Guid? OrderId { get; set; }

    public Guid? ActId { get; set; }

    public int? Action { get; set; }

    public string? Number { get; set; }

    public short Year { get; set; }

    public DateTime Date { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public Guid? SyndicId { get; set; }

    public Guid AppealKindId { get; set; }

    public virtual Act? Act { get; set; }

    public virtual NomAppealKind AppealKind { get; set; } = null!;

    public virtual Order? Order { get; set; }

    public virtual ICollection<OutgoingDocument> OutgoingDocuments { get; set; } = new List<OutgoingDocument>();

    public virtual ICollection<Side> Sides { get; set; } = new List<Side>();

    public virtual Syndic? Syndic { get; set; }
}
