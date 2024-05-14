using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.Common.Helper.ApiRequestIntercepting
{
    public enum eProcessingStatus
    {
        Pending = 0,
        InProcess = 1,
        Finished = 2,
        Killed = 3,
    }
}
