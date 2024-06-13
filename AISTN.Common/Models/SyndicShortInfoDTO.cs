namespace AISTN.Common.Models
{
    public class SyndicShortInfoDTO
    {
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public string? Egn { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public Guid? UserId { get; set; }
    }
}
