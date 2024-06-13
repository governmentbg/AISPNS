namespace AISTN.ExternalAppAPI.Models.ePayment
{
    public class CreditTransferOrderDTO
    {
        public string PaymentCode { get; set; }
        public double CreditTransferAmount { get; set; }
        public string PaymentReason { get; set; }
        public PaymentReferenceDTO PaymentReference { get; set; }
        public PaymentPeriodDTO PaymentPeriod { get; set; }
    }
}
