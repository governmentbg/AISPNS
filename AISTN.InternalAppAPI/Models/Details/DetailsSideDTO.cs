using AISTN.Common.Models;
using AISTN.Data.DataModel;

namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsSideDTO
    {
        public Guid Id { get; set; }

        public Guid? CaseId { get; set; }

        public string? DebtorStatus { get; set; }

        public bool IsDebtor { get; set; }

        public NomenclatureDTO? InvolvementKind { get; set; }

        public DetailsPersonDTO? Person { get; set; }

        public DetailsEntityDTO? Entity { get; set; }
    }
}
