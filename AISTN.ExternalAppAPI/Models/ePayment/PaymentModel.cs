using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.ExternalAppAPI.ExternalAppAPI.ePayment
{
    public class PaymentModel
    {
        public string serviceProviderName { get; set; }
        public string serviceProviderBank { get; set; }
        public string serviceProviderBIC { get; set; }
        public string serviceProviderIBAN { get; set; }
        public string currency { get; set; }
        public Double paymentAmount { get; set; }
        public string paymentReason { get; set; }
    }
}
