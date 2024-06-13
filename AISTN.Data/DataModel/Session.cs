using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Session
{
    public Guid Id { get; set; }

    public Guid CaseId { get; set; }

    public Guid? ActionId { get; set; }

    public Guid? SyndicActionId { get; set; }

    public Guid? SessionKindId { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? Time { get; set; }

    public Guid? ResultId { get; set; }

    public string? Text { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual NomAction? Action { get; set; }

    public virtual ICollection<Act> Acts { get; set; } = new List<Act>();

    public virtual Case Case { get; set; } = null!;

    public virtual ICollection<Judge> Judges { get; set; } = new List<Judge>();

    public virtual NomSessionResult? Result { get; set; }

    public virtual NomSessionKind? SessionKind { get; set; }

    public virtual NomSyndicAction? SyndicAction { get; set; }
}
