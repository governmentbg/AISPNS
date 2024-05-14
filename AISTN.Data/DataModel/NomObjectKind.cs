﻿using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomObjectKind
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Object> Objects { get; set; } = new List<Object>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
