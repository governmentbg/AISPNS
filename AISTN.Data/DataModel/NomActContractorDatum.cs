using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomActContractorDatum
{
    public Guid Id { get; set; }

    public bool? IsStabilization { get; set; }

    public string? Description { get; set; }
}
