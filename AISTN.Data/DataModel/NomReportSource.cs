using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomReportSource
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<StatisticalReport> StatisticalReports { get; set; } = new List<StatisticalReport>();
}
