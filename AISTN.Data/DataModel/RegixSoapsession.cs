using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class RegixSoapsession
{
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public Guid UserId { get; set; }

    public string? Ipaddress { get; set; }

    public virtual ICollection<RegixSoaprequest> RegixSoaprequests { get; set; } = new List<RegixSoaprequest>();

    public virtual RegixSoapuser User { get; set; } = null!;
}
