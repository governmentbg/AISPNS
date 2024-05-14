using AISTN.Common.Models;
using AISTN.Data.DataModel;

namespace AISTN.InternalAppAPI.Models.Index
{
    public class SyndicIndexDTO
    {
        public Guid Id { get; set; }

        public string? Number { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string? SecondLastName { get; set; }

        public string? Egn { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? SyndicSpecialty { get; set; }

        public string? SyndicStatus { get; set; }

        public bool? Active { get; set; }

        public bool? Locked { get; set; }

        public bool? IsCustodian { get; set; }

        public string? LastInstallmentForYear { get; set; }

        public string? InstallmentForYears { get; set; }

        public string? Notes { get; set; }

        public string? FullAddress { get; set; }

        public IEnumerable<OrderIndexDTO>? Orders { get; set; }
    }
}
