namespace AISTN.Common.Models
{
    public class SessionDTO
    {
        public Guid Id { get; set; }

        public Guid CaseId { get; set; }

        public Guid? ActionId { get; set; }

        public Guid? SyndicActionId { get; set; }

        public Guid? SessionKindId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Time { get; set; }

        public Guid? ResultId { get; set; }

        public string? Text { get; set; }
    }
}
