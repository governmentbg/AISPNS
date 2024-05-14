using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Index
{
    public class AnnouncementIndexDTO
    {
        public Guid Id { get; set; }

        public DateTime OfferingDate { get; set; }

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

        public CaseIndexDTO? Case { get; set; }

        public SyndicIndexDTO? Syndic { get; set; }

        public AnnouncementNomenclatureDTO? LocationType { get; set; }

        public string? FullAddress { get; set; }
    }
}
