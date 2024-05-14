using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.Common.Models
{
    public class SaveDocumentContentDTO
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public DateTime? DocumentDate { get; set; }

        public Guid? AttachedDocumentKindId { get; set; }

        public Guid? SignalDocumentKindId { get; set; }
    }
}
