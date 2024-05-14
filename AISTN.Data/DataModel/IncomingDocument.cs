using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class IncomingDocument
{
    public Guid Id { get; set; }

    public Guid CaseId { get; set; }

    public Guid KindId { get; set; }

    public short Number { get; set; }

    public short Year { get; set; }

    public DateTime Date { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual NomIncomingDocumentKind Kind { get; set; } = null!;
}
