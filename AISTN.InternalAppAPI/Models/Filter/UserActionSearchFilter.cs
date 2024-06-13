namespace AISTN.InternalAppAPI.Models.Filter
{
    public class UserActionSearchFilter
    {
        public string? IpAddress { get; set; }

        public string? UserAction { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? TimestampFrom { get; set; }

        public DateTime? TimestampTo { get; set; }
    }
}
