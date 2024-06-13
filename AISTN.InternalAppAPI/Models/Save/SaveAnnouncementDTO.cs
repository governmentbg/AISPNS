using AISTN.Common.Models;
using AISTN.InternalAppAPI.Models.Index;

namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveAnnouncementDTO
    {
        public Guid Id { get; set; }

        public Guid CaseId { get; set; }

        public DateTime Date { get; set; }

        public DateTime OfferingDate { get; set; }

        public Guid? LegalBasisId { get; set; }

        public DateTime OfferDeadline { get; set; }

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

        public IEnumerable<Announcement2AddressDTO>? Announcement2Addresses { get; set; }

        public CaseIndexDTO? Case { get; set; }

        public NomLegalBasisDTO? LegalBasis { get; set; }

        public NomOfferingLocationTypeDTO? LocationType { get; set; }

        public NomOfferingKindDTO? OfferingKind { get; set; }

        public NomOfferingProcedureDTO? OfferingProcedure { get; set; }

        public NomPapersForSaleDTO? PapersForSale { get; set; }

        public SaveAccountUserDTO? PublishedByNavigation { get; set; }

        public NomSalesProcedureDTO? SalesProcedure { get; set; }

        public SaveSyndicDTO? Syndic { get; set; }

        public SaveSyndicDTO? SecondSyndic { get; set; }

        public NomenclatureDTO? StatusCodeNavigation { get; set; }
    }
}
