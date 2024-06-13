using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.Common.Models.Save
{
    public class SaveEntityDTO
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Bulstat { get; set; }
    }
}
