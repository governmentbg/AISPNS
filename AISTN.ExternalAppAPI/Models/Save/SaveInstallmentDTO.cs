using AISTN.ExternalAppAPI.ExternalAppAPI.ePayment;

namespace AISTN.ExternalAppAPI.Models.Save
{
    public class SaveInstallmentDTO
    {
        public Guid? Id { get; set; }

        public Guid? SyndicId { get; set; }

        public Guid? InstallmentKindId { get; set; }

        public Guid? InstallmentYearId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? Bank { get; set; }

        public decimal? Amount { get; set; }

        public string? Number { get; set; }

        public bool? Verified { get; set; }

        public Guid? VerifiedBy { get; set; }

        public string? VerifiedByName { get; set; }

        public DateTime? TerminationDeadline { get; set; }

        public string? Note { get; set; }

        public string? PaymentRequestId { get; set; }

        public string? PaymentAccessCode { get; set; }

        public string? PaymentStatus { get; set; }

        public Guid? DocumentCollectionId { get; set; }
    }
}
