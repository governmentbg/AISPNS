using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Installment
{
    public Guid Id { get; set; }

    public Guid SyndicId { get; set; }

    public Guid? InstallmentKindId { get; set; }

    public Guid? InstallmentYearId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Bank { get; set; }

    public decimal? Amount { get; set; }

    public string? Number { get; set; }

    public bool? Verified { get; set; }

    public Guid? VerifiedBy { get; set; }

    public DateTime? TerminationDeadline { get; set; }

    public string? Note { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public string? PaymentRequestId { get; set; }

    public string? PaymentAccessCode { get; set; }

    public bool? PaymentCompletedFlag { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? PaymentStatusDate { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual NomInstallmentKind? InstallmentKind { get; set; }

    public virtual NomInstallmentYear? InstallmentYear { get; set; }

    public virtual Syndic Syndic { get; set; } = null!;

    public virtual User? VerifiedByNavigation { get; set; }
}
