using AISTN.Common.Models;
using AISTN.Data.DataModel;

namespace AISTN.InternalAppAPI.Models.Index
{
    public class InspectionIndexDTO
    {
        public Guid Id { get; set; }

        public DateTime? InspectionDate { get; set; }

        public string? SyndicName { get; set; }

        public string? InspectorName { get; set; }

        public string? InspectionTypeDescription { get; set; }
    }
}
