using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class StatTemplate
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }
}
