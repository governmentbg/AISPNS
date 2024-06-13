namespace AISTN.Common.Models
{
    public class ReceiverIndexDTO
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public string? FullName { get; set; }

        public List<string>? Roles { get; set; }
    }
}
