namespace AISTN.Common.Models
{
    public class NomSettlementDTO
    {
        public Guid Id { get; set; }

        public Guid? MunicipalityId { get; set; }

        public string LauCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public string FullPathName { get; set; } = null!;

        public string FullPath { get; set; } = null!;

        public decimal Order { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastModified { get; set; }

        public int? ViewOrder { get; set; }
    }
}
