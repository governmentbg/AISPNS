using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.Common.Helper.ApiRequestIntercepting
{
    public class ProcessingSettings
    {
        public int MaxRequestsInProcess { get; set; }

        public int ProcessingTimeoutSeconds { get; set; }
    }
}
