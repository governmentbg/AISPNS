using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class OutgoingDocument
{
    public Guid Id { get; set; }

    public Guid AppealId { get; set; }

    public Guid? ActionId { get; set; }

    public Guid SendKindId { get; set; }

    public string Number { get; set; } = null!;

    public short Year { get; set; }

    public Guid CourtId { get; set; }

    public DateTime Date { get; set; }

    public string? ReturnResult1 { get; set; }

    public string? ReturnResult2 { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual NomAction? Action { get; set; }

    public virtual Appeal Appeal { get; set; } = null!;

    public virtual NomCourt Court { get; set; } = null!;

    public virtual NomSendToKind SendKind { get; set; } = null!;
}
