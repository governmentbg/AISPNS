using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class RegixUnrecognizedSession
{
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }

    public string? ProvidedToken { get; set; }

    public string? Ipaddress { get; set; }
}
