using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Index
{
    public class SideIndexDTO
    {
        public Guid Id { get; set; }

        public NomenclatureDTO? InvolvementKind { get; set; }

        public EntityIndexDTO? Entity { get; set; }

        public PersonIndexDTO? Person { get; set; }
    }
}
