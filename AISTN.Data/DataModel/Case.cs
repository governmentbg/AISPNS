using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Case
{
    public Guid Id { get; set; }

    public Guid CourtId { get; set; }

    public Guid? CaseKindId { get; set; }

    public Guid? CaseCodeId { get; set; }

    public string? StatCode { get; set; }

    public string Number { get; set; } = null!;

    public short Year { get; set; }

    public DateTime? FormationDate { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public bool? IsStabilization { get; set; }

    public bool? IsProprietor { get; set; }

    public Guid? SyndicCaseReportId { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual ICollection<CaseAssignment> CaseAssignments { get; set; } = new List<CaseAssignment>();

    public virtual NomCaseCode? CaseCode { get; set; }

    public virtual NomCaseKind? CaseKind { get; set; }

    public virtual NomCourt Court { get; set; } = null!;

    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();

    public virtual ICollection<ImportedDeed> ImportedDeeds { get; set; } = new List<ImportedDeed>();

    public virtual ICollection<IncomingDocument> IncomingDocuments { get; set; } = new List<IncomingDocument>();

    public virtual ICollection<Judge> Judges { get; set; } = new List<Judge>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Side> Sides { get; set; } = new List<Side>();

    public virtual ICollection<Signal> Signals { get; set; } = new List<Signal>();

    public virtual ICollection<SurroundDocument> SurroundDocuments { get; set; } = new List<SurroundDocument>();

    public virtual NomSyndicCaseReport? SyndicCaseReport { get; set; }
}
