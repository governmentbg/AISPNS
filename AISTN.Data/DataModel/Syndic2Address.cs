using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Syndic2Address
{
    public Guid Id { get; set; }

    public Guid? SyndicId { get; set; }

    public Guid? AddressId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Syndic? Syndic { get; set; }
}
