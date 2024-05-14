using AISTN.Common.Models;
using AISTN.InternalAppAPI.Models.Index;

namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveObjectDTO
    {
        public Guid Id { get; set; }

        public int ObjectTypeCode { get; set; }

        public Guid ObjectKindId { get; set; }

        public Guid? AddressId { get; set; }

        public string? Condition { get; set; }

        public string? Notes { get; set; }

        public string? Value { get; set; }

        public Guid AnnouncementId { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public IEnumerable<Object2AddressDTO> Object2Addresses { get; set; }

        public NomObjectKindDTO ObjectKind { get; set; } = null!;

        public NomObjectTypeDTO ObjectTypeCodeNavigation { get; set; } = null!;
    }
}
