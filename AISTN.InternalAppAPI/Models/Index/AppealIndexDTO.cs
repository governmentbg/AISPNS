using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Index
{
    public class AppealIndexDTO
    {
        public Guid? Id { get; set; }

        public string? OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? Number { get; set; }

        public DateTime? Date { get; set; }

        public int? Action { get; set; }

        public string? AppealKind { get; set; }

        public DocumentCollectionDTO? Documents { get; set; }
    }
}
