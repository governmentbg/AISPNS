using AISTN.Common.Models;
using AISTN.InternalAppAPI.Models.Save;

namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsSyndicDTO
    {
        public Guid Id { get; set; }

        public string? Number { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string? Egn { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? SyndicSpecialty { get; set; }

        public string? SyndicStatus { get; set; }

        public bool? Active { get; set; }

        public bool? Locked { get; set; }

        public int? LastInstallmentForYear { get; set; }

        public string? InstallmentForYears { get; set; }

        public string? Notes { get; set; }

        public Guid? Specialty { get; set; }

        public Guid? SyndicStatusId { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public AddressIndexDTO? Address { get; set; }

        public SaveOrderDTO? Order { get; set; }
    }
}
