using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Report
{
    public Guid Id { get; set; }

    public Guid ReportTypeId { get; set; }

    public DateTime ReportDate { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string? Notes { get; set; }

    public Guid SyndicId { get; set; }

    public Guid CaseId { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public Guid? AttachedDocumentCollectionId { get; set; }

    public Guid? SampleId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual DocumentCollection? AttachedDocumentCollection { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual NomSyndicCaseReport ReportType { get; set; } = null!;

    public virtual Sample? Sample { get; set; }

    public virtual Syndic Syndic { get; set; } = null!;
}
