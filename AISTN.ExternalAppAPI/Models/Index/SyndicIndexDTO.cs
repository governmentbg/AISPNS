using AISTN.Common.Models;

namespace AISTN.ExternalAppAPI.Models.Index
{
    public class SyndicIndexDTO
    {
        public Guid Id { get; set; }

        public string? Number { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string? Egn { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int? Status { get; set; }

        public string? SyndicSpecialty { get; set; }

        public bool? Active { get; set; }

        public bool? Locked { get; set; }

        public bool? IsCustodian { get; set; }

        public int? LastInstallmentForYear { get; set; }

        public string? InstallmentForYears { get; set; }

        public AddressIndexDTO? Address { get; set; }
    }
}