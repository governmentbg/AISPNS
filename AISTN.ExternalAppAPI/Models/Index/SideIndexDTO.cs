using AISTN.Common.Models;

namespace AISTN.ExternalAppAPI.Models.Index
{
    public class SideIndexDTO
    {

        public Guid? Id { get; set; }

        public NomenclatureDTO? InvolvementKind { get; set; }

        public EntityIndexDTO? Entity { get; set; }
    }
}
