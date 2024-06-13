using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class RegixFailedLogin
{
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }

    public string? ProvidedUserName { get; set; }

    public string? ProvidedPassword { get; set; }

    public string? Ipaddress { get; set; }
}
