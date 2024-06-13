using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Signal
{
    public Guid Id { get; set; }

    public string Number { get; set; } = null!;

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public Guid? CaseId { get; set; }

    public Guid? SenderId { get; set; }

    public Guid? SyndicId { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public virtual Case? Case { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual SignalSender? Sender { get; set; }

    public virtual Syndic? Syndic { get; set; }
}
