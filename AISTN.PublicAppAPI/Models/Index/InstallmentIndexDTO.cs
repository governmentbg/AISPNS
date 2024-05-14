namespace AISTN.PublicAppAPI.Models.Index
{
    public class InstallmentIndexDTO
    {
        public Guid Id { get; set; }

        public Guid SyndicId { get; set; }

        public Guid Syndic { get; set; }

        public string? Kind { get; set; }

        public string? PaymentDate { get; set; }

        public string? Bank { get; set; }

        public decimal? Amount { get; set; }

        public bool? Verified { get; set; }

        public int? VerifiedBy { get; set; }

        public DateTime? TerminationDeadline { get; set; }

        public string? Note { get; set; }
    }
}
