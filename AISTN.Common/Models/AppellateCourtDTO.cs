using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.Common.Models
{
    public class AppellateCourtDTO
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string AppellateCourtName { get; set; }

        public string CourtName { get; set; }
    }
}
