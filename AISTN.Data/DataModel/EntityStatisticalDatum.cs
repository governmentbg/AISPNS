using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class EntityStatisticalDatum
{
    public Guid Id { get; set; }

    public Guid EntityId { get; set; }

    public int? NumberJobCuts { get; set; }

    public bool? WasRestructured { get; set; }

    public Guid? CompanySizeId { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual NomCompanySize? CompanySize { get; set; }

    public virtual Entity Entity { get; set; } = null!;
}
