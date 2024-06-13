using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomReportType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<StatisticalReport> StatisticalReports { get; set; } = new List<StatisticalReport>();
}
