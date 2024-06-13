using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.ExternalAppAPI.ExternalAppAPI.ePayment
{
    public class PaymentResponseVPost
    {
        public string ErrorMessage { get; set; }
        public string RequestId { get; set; }
        public string ResultTime { get; set; }
        public string Status { get; set; }
        public string VposResultGid { get; set; }
    }
}
