using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.ExternalAppAPI.ExternalAppAPI.ePayment
{
    public class PaymentRequestPublicData
    {
        public string clientId { get; set; }
        public string hmac { get; set; }
        public string Data { get; set; }
    }
}
