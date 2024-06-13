namespace AISTN.InternalAppAPI.Models.Filter
{
    public class UserFilterDTO
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Egn { get; set; }
        public string? Email { get; set; }
        public Guid? RoleId { get; set; }
        public bool? IsActive { get; set; }
    }
}
