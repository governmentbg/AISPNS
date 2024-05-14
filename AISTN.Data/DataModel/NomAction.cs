using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomAction
{
    public Guid Id { get; set; }

    public int Code { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<OutgoingDocument> OutgoingDocuments { get; set; } = new List<OutgoingDocument>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<SurroundDocument> SurroundDocuments { get; set; } = new List<SurroundDocument>();
}
