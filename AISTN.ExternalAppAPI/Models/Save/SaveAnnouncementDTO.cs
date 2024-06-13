using AISTN.Common.Models;
using AISTN.ExternalAppAPI.Models.Index;

namespace AISTN.ExternalAppAPI.Models.Save
{
    public class SaveAnnouncementDTO
    {
        public Guid? Id { get; set; }

        public Guid? CaseId { get; set; }

        public DateTime? OfferingDate { get; set; }

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

        public string? CorrectionComment { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public Guid? PublishedBy { get; set; }

        public int? StatusCode { get; set; }

        public AddressIndexDTO? Address { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public Guid? CourtId { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public string? DebtorName { get; set; }

        public string? DebtorEik { get; set; }
    }
}
