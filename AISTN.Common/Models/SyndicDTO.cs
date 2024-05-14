namespace AISTN.Common.Models
{
    public class SyndicDTO
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

        public AddressIndexDTO? Address { get; set; }

        public OrderIndexDTO? Order { get; set; }
    }
}
