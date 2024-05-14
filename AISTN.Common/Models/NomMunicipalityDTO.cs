namespace AISTN.Common.Models
{
    public class NomMunicipalityDTO
    {
        public Guid Id { get; set; }

        public Guid? DistrictId { get; set; }

        public string? LauCode { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? FullPathName { get; set; }

        public string? FullPath { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastModified { get; set; }

        public int? ViewOrder { get; set; }
    }
}
