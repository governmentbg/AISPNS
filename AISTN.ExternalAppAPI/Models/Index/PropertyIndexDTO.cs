using AISTN.Common.Models;

namespace AISTN.ExternalAppAPI.Models.Index
{
    public class PropertyIndexDTO
    {
        public Guid Id { get; set; }

        public Guid? CaseId { get; set; }

        public Guid? PropertyClassId { get; set; }

        public Guid? PropertyTypeId { get; set; }

        public string? PropertyTypeName { get; set; }

        public Guid? PropertyKindId { get; set; }

        public string? PropertyKindName { get; set; }

        public Guid? AddressId { get; set; }

        public string? FullAddress { get; set; }

        public string? State { get; set; }

        public string? Notes { get; set; }

        public string? Value { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public Guid? PersonId { get; set; }
        public string? PersonName { get; set; }

        public Guid? EntityId { get; set; }
        public string? EntityName { get; set; }
    }
}
