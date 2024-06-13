using AISTN.Common.Models;

namespace AISTN.ExternalAppAPI.Models.Index
{
    public class CaseIndexDTO
    {
        public Guid? Id { get; set; }

        public string? StatCode { get; set; }

        public string? Number { get; set; }

        public short? Year { get; set; }

        public DateTime? FormationDate { get; set; }

        public NomCourtDTO? Court { get; set; }

        public string? CaseKindDescription { get; set; }

        public SideIndexDTO? Side { get; set; }
    }
}
