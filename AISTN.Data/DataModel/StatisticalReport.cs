using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class StatisticalReport
{
    public Guid Id { get; set; }

    public string? IdentificationNumbr { get; set; }

    public Guid? Type { get; set; }

    public Guid? ReportSourceId { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? Note { get; set; }

    public string? ReportPreparator { get; set; }

    public DateTime? Published { get; set; }

    public Guid? Template { get; set; }

    public bool? IsPublished { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual NomReportSource? ReportSource { get; set; }

    public virtual NomReportType? TypeNavigation { get; set; }
}
