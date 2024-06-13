using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsPropertyDTO
    {
        public Guid? Id { get; set; }

        public Guid? CaseId { get; set; }

        public Guid? PropertyClassId { get; set; }

        public Guid? PropertyTypeId { get; set; }

        public Guid? PropertyKindId { get; set; }

        public Guid? AddressId { get; set; }

        public string? State { get; set; }

        public string? Notes { get; set; }

        public string? Value { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? EntityId { get; set; }

        public AddressIndexDTO? Address { get; set; }
    }
}
