namespace AISTN.ExternalAppAPI.Models.ePayment
{
    public class PaymentReferenceDTO
    {
        public string PaymentReferenceType { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public DateTime? PaymentReferenceDate { get; set; }
    }
}