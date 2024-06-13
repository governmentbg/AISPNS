namespace AISTN.Common.Models
{
    public class MessageIndexDTO
    {
        public Guid? Id { get; set; }

        public DateTime? SendDate { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public string? ReceiverName { get; set; }

        public string? SenderName { get; set; }

        public Boolean? Seen { get; set; }

        public string? Subject { get; set; }

        public Guid ReplyId { get; set; }

        public Guid ParentId { get; set; }
    }
}
