using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSendToKind
{
    public Guid Id { get; set; }

    public int Code { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<OutgoingDocument> OutgoingDocuments { get; set; } = new List<OutgoingDocument>();
}
