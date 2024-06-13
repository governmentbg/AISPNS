using AISTN.Common.Models;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Index;

namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsCaseDTO
    {
        public Guid? Id { get; set; }

        public string? StatCode { get; set; }

        public string? Number { get; set; }

        public short? Year { get; set; }

        public DateTime? FormationDate { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsStabilization { get; set; }

        public bool? IsProprietor { get; set; }

        public IEnumerable<DetailsSideDTO?> Sides { get; set; }

        public NomCourtDTO? Court { get; set; }

        public NomenclatureDTO? CaseKind { get; set; }

        public NomCaseCodeDTO? CaseCode { get; set; }

        public IEnumerable<DetailsSessionDTO?> Sessions { get; set; }

        public IEnumerable<DetailsJudgeDTO?> Judges { get; set; }
    }
}
