using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSyndicCaseReport
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();

    public virtual ICollection<ReportTemplate> ReportTemplates { get; set; } = new List<ReportTemplate>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
