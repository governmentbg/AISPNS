namespace AISTN.ExternalAppAPI.Models.ePayment
{
    public class EPaymentsRequestDTO
    {

        #region DocumentTypeURI
        public string RegisterIndex { get; set; }
        public string BatchNumber { get; set; }

        public string DocumentTypeName { get; set; }
        public string PaymentCode { get; set; }
        public string Currency { get; set; }
        public double PaymentAmount { get; set; }
        #endregion

        #region ElectronicServiceProviderBasicData
        public string ElectronicServiceProviderType { get; set; }
        public string ElectronicServiceProviderName { get; set; }
        public string ElectronicServiceProviderIdentifier { get; set; }
        #endregion

        #region ElectronicServiceProviderBankAccount
        public string BIC { get; set; }
        public string IBAN { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountIdentifier { get; set; }
        #endregion


        #region ElectronicServiceRecipient
        public ElectronicServiceRecipientDTO ElectronicServiceRecipient { get; set; }
        #endregion

        public string PaymentReason { get; set; }

        public PaymentReferenceDTO PaymentReference { get; set; }

        public PaymentPeriodDTO PaymentPeriod { get; set; }

        public double? PaymentExpirationDays { get; set; } //should be negative number!

        public DateTime? PaymentRequestExpirationDate { get; set; }

        public string AdditionalInformationInPaymentRequest { get; set; }

        public string SUNAUServiceURI { get; set; }

        public string ElectronicAdministrativeServiceSupplierUriRA { get; set; }

        public string ElectronicAdministrativeServiceNotificationURL { get; set; }

        public int? ObligationId { get; set; }

        public ICollection<CreditTransferOrderDTO> CreditTransferOrders { get; set; }
    }
}
