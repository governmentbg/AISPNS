using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Announcement
{
    public Guid Id { get; set; }

    public Guid? CaseId { get; set; }

    public DateTime OfferingDate { get; set; }

    public Guid? LegalBasisId { get; set; }

    public DateTime? OfferDeadline { get; set; }

    public Guid? LocationTypeId { get; set; }

    public Guid? SyndicId { get; set; }

    public Guid? SecondSyndicId { get; set; }

    public Guid? OfferingProcedureId { get; set; }

    public Guid? OfferingKindId { get; set; }

    public string? StartingPrice { get; set; }

    public string? Retainer { get; set; }

    public Guid? PapersForSaleId { get; set; }

    public Guid? SalesProcedureId { get; set; }

    public string? ParticipationLimitations { get; set; }

    public string? PricePaymentTerms { get; set; }

    public string? DeterminationFollowupBuyers { get; set; }

    public string? Awarded { get; set; }

    public string? RiskTransfer { get; set; }

    public string? CoownershipSale { get; set; }

    public string? MortgagedPropertySale { get; set; }

    public string? Text { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public bool IsActive { get; set; }

    public DateTime? PublishDate { get; set; }

    public DateTime? ReceivedDate { get; set; }

    public Guid? PublishedBy { get; set; }

    public int StatusCode { get; set; }

    public Guid? AddressId { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public string? CorrectionComment { get; set; }

    public Guid? SendForCorrectionBy { get; set; }

    public Guid? CourtId { get; set; }

    public string? CaseNumber { get; set; }

    public short? CaseYear { get; set; }

    public string? DebtorName { get; set; }

    public string? DebtorEik { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Case? Case { get; set; }

    public virtual NomCourt? Court { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual NomLegalBasis? LegalBasis { get; set; }

    public virtual NomOfferingLocationType? LocationType { get; set; }

    public virtual ICollection<Object> Objects { get; set; } = new List<Object>();

    public virtual NomOfferingKind? OfferingKind { get; set; }

    public virtual NomOfferingProcedure? OfferingProcedure { get; set; }

    public virtual NomPapersForSale? PapersForSale { get; set; }

    public virtual User? PublishedByNavigation { get; set; }

    public virtual NomSalesProcedure? SalesProcedure { get; set; }

    public virtual Syndic? SecondSyndic { get; set; }

    public virtual User? SendForCorrectionByNavigation { get; set; }

    public virtual NomAnnouncementStatus StatusCodeNavigation { get; set; } = null!;

    public virtual Syndic? Syndic { get; set; }
}
