using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsObjectDTO
    {
        public Guid? Id { get; set; }

        public string? ObjectType { get; set; }

        public string? ObjectKind { get; set; }

        public string? Condition { get; set; }

        public string? Notes { get; set; }

        public string? Value { get; set; }

        public DocumentCollectionDTO? DocumentCollection { get; set; }

        public DetailsAddressIndexDTO? Address { get; set; }
    }
}
