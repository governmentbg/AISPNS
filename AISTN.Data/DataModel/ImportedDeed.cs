using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class ImportedDeed
{
    public Guid Id { get; set; }

    public DateTime ImportDate { get; set; }

    public Guid ImportRequestId { get; set; }

    public string DeedRawXml { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string? CompanyLegalForm { get; set; }

    public string? CompanyUic { get; set; }

    public string? FieldEntryNumber { get; set; }

    public DateTime? FieldActionDate { get; set; }

    public DateTime? FieldEntryDate { get; set; }

    public string? CaseNumber { get; set; }

    public int? CaseYear { get; set; }

    public DateTime? ActDate { get; set; }

    public string? ActNumber { get; set; }

    public string? ActType { get; set; }

    public int? ActComplaintTerm { get; set; }

    public int? CourtNo { get; set; }

    public string? DeedGuid { get; set; }

    public Guid? CaseId { get; set; }

    public Guid? CourtofAppealId { get; set; }

    public virtual ICollection<ActDetail> ActDetails { get; set; } = new List<ActDetail>();

    public virtual Case? Case { get; set; }

    public virtual NomCourt? CourtofAppeal { get; set; }

    public virtual ImportRequest ImportRequest { get; set; } = null!;

    public virtual ICollection<Trustee> Trustees { get; set; } = new List<Trustee>();
}
