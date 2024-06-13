namespace AISTN.Common.Models.Save
{
    public class SaveAddressDTO
    {
        public Guid? RegionId { get; set; }

        public Guid? MunicipalityId { get; set; }

        public Guid? SettlementId { get; set; }

        public string? PostCode { get; set; }

        public string? StreetNumber { get; set; }

        public string? StreetName { get; set; }

        public string? BuildingNumber { get; set; }

        public string? EntranceNumber { get; set; }

        public string? FloorNumber { get; set; }

        public string? ApartmentNumber { get; set; }

        public string? Additional { get; set; }

        public int? Ekkate { get; set; }
    }
}
