namespace AISTN.InternalAppAPI.Models.Index
{
    public class InstallmentIndexDTO
    {
        public Guid Id { get; set; }

        public string? SyndicName { get; set; }

        public string? InstallmentKindName { get; set; }

        public int? InstallmentYear { get; set; }

        public decimal? InstallmentYearAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? Bank { get; set; }

        public decimal? Amount { get; set; }

        public string? Number { get; set; }

        public bool? Verified { get; set; }

        public string? VerifiedByName { get; set; }

        public DateTime? TerminationDeadline { get; set; }

        public string? Note { get; set; }
    }
}
