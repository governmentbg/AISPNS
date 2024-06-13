using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsAnnouncementDTO
    {
        public Guid? Id { get; set; }

        public DateTime? OfferingDate { get; set; }

        public DateTime? OfferDeadline { get; set; }

        public string? StartingPrice { get; set; }

        public string? Retainer { get; set; }

        public string? ParticipationLimitations { get; set; }

        public string? PricePaymentTerms { get; set; }

        public string? DeterminationFollowupBuyers { get; set; }

        public string? Awarded { get; set; }

        public string? RiskTransfer { get; set; }

        public string? CoownershipSale { get; set; }

        public string? MortgagedPropertySale { get; set; }

        public string? Text { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public string? Status { get; set; }

        public DetailsCaseDTO? Case { get; set; }

        public string? LegalBasis { get; set; }

        public string? LocationType { get; set; }

        public DetailsAddressIndexDTO? Address { get; set; }

        public DetailsSyndicDTO? Syndic { get; set; }

        public DetailsSyndicDTO? SecondSyndic { get; set; }

        public string? OfferingProcedure { get; set; }

        public string? OfferingKind { get; set; }

        public string? PapersForSale { get; set; }

        public string? SalesProcedure { get; set; }

        public IEnumerable<DetailsObjectDTO>? Objects { get; set; }

        public DocumentCollectionDTO? DocumentCollection { get; set; }
    }
}
