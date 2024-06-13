using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.ExternalAppAPI.ExternalAppAPI.ePayment
{
    public class PaymentInfoModel
    {
        public string ServiceProviderName { get; set; }
        public string ServiceProviderBank { get; set; }
        public string ServiceProviderBIC { get; set; }
        public string ServiceProviderIBAN { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string PaymentCode { get; set; }
        public string AccessCode { get; set; }
        public PaymentRequestPublicData vpos { get; set; }
        public PaymentRequestPublicData order { get; set; }
    }
}
