namespace AISTN.InternalAppAPI.Models.Index
{
    public class UserActionIndexDTO
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string? UserName { get; set; }

        public string? UserActionType { get; set; }

        public DateTime? Timestamp { get; set; }

        public string? Message { get; set; }

        public string? IpAddress { get; set; }
    }
}
