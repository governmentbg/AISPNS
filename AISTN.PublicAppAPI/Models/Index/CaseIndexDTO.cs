using AISTN.Common.Models;

namespace AISTN.PublicAppAPI.Models.Index
{
    public class CaseIndexDTO
    {
        public Guid? Id { get; set; }

        public string? Number { get; set; }

        public short? Year { get; set; }

        public DateTime? FormationDate { get; set; }

        public string? CourtName { get; set; }

        public string? CaseKindDescription { get; set; }

        public string? SideName { get; set; }

        public string? Bulstat { get; set; }
    }
}
