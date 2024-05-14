using AISTN.Common.Models;

namespace AISTN.PublicAppAPI.Models.Index
{
    public class AnnouncementCaseIndexDTO
    {
        public Guid? Id { get; set; }

        public string? StatCode { get; set; }

        public string? Number { get; set; }

        public short? Year { get; set; }

        public DateTime? FormationDate { get; set; }

        public NomCourtDTO? Court { get; set; }

        public NomenclatureDTO? CaseKind { get; set; }

        public SideIndexDTO? Side { get; set; }
    }
}
