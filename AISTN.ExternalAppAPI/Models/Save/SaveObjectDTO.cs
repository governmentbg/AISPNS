using AISTN.Common.Models;

namespace AISTN.ExternalAppAPI.Models.Save
{
    public class SaveObjectDTO
    {
        public Guid? Id { get; set; }

        public Guid? ObjectTypeId { get; set; }

        public Guid? ObjectKindId { get; set; }

        public string? Condition { get; set; }

        public string? Notes { get; set; }

        public string? Value { get; set; }

        public Guid? AnnouncementId { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public AddressIndexDTO? Address { get; set; }
    }
}
