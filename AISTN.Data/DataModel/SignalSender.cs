using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class SignalSender
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? CitizenshipNumber { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public Guid? AddressId { get; set; }

    public Guid? SignalSenderTypeId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual NomSignalSenderType? SignalSenderType { get; set; }

    public virtual ICollection<Signal> Signals { get; set; } = new List<Signal>();
}
