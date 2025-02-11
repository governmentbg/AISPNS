﻿using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomOrderPaymentKind
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
