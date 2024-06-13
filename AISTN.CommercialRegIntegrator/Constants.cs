using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.CommercialRegIntegrator
{
    public static class Constants
    {
        public static class XPathQueries
        {
            public const string DeedNameSpace = @"http://www.registryagency.bg/schemas/deedv2";
            public const string FieldNameSpace = @"http://www.registryagency.bg/schemas/deedv2/Fields";

        }
        public static class ImportRequestStatuses
        {
            public const int IN_PROGRESS = 1;
            public const int SUCCESS = 2;
            public const int DUPLICATE_CONTENT = 3;
            public const int GENERAL_ERROR = 4;

        }



    }
}
