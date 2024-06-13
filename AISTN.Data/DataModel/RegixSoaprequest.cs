using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class RegixSoaprequest
{
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid SessionId { get; set; }

    public string? StateNumber { get; set; }

    public string? InvolvmentKind { get; set; }

    public int? ResultsCount { get; set; }

    public virtual RegixSoapsession Session { get; set; } = null!;
}
