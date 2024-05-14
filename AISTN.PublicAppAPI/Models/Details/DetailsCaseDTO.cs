using AISTN.Common.Models;

namespace AISTN.PublicAppAPI.Models.Details
{
    public class DetailsCaseDTO
    {
        public string? CourtName { get; set; }

        public string? Number { get; set; }

        public short? Year { get; set; }

        public DateTime? FormationDate { get; set; }

        public string? CaseKindDescription { get; set; }

        public IEnumerable<DetailsSideDTO?> Sides { get; set; }
    }
}
