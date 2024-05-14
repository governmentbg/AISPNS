using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.ExternalAppAPI.ExternalAppAPI.ePayment
{
    public class PaymentResultData
    {
        public bool result { get; set; }
        public Guid applicationGuid { get; set; }
        public string message { get; set; }
        public Guid FormTypeGuid { get; set; }
    }
}
