using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSignalSenderType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<SignalSender> SignalSenders { get; set; } = new List<SignalSender>();
}
