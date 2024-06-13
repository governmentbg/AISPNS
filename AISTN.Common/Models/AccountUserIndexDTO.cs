namespace AISTN.Common.Models
{
    public class AccountUserIndexDTO
    {
        public Guid? Id { get; set; }

        public string? UserName { get; set; }

        public bool? IsActive { get; set; }

        public int? PersonId { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string? Egn { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public List<string>? RoleNames { get; set; }
    }
}
