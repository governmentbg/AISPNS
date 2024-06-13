using AISTN.Common.Models;
using AISTN.Data.DataModel;

namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsSessionDTO
    {
        public Guid? Id { get; set; }

        public Guid? CaseId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Time { get; set; }

        public string? Text { get; set; }

        public NomAction? Action { get; set; }

        public NomenclatureDTO? SessionKind { get; set; }

        public NomenclatureDTO? Result { get; set; }
    }
}
