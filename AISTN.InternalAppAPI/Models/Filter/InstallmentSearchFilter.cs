namespace AISTN.InternalAppAPI.Models.Filter
{
    public class InstallmentSearchFilter
    {
        public string? InstallementKind { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? Bank { get; set; }

        public decimal? Amount { get; set; }
    }
}
