using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Save
{
    public class SavePleaDTO
    {
        public Guid? Id { get; set; }

        public string? OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? PleaNumber { get; set; }

        public DateTime? PleaDate { get; set; }

        public string? CourtDecision { get; set; }

        public Guid? SyndicId { get; set; }

        public Guid? DocumentCollectionId { get; set; }
    }
}
