using AISTN.Data.LogDataModel;

namespace AISTN.InternalAppAPI.Models.Index
{
    public class LogDbcontextIndexDTO
    {
        public int? Id { get; set; }

        public string? ActionTable { get; set; } = null!;

        public string? TableRow { get; set; }

        public string? ActionType { get; set; } = null!;

        public DateTime? Timestamp { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public int? UserActionLogId { get; set; }
    }
}
