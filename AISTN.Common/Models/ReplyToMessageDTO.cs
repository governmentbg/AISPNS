namespace AISTN.Common.Models
{
    public class ReplyToMessageDTO
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public IEnumerable<FileDTO>? Files { get; set; }
    }
}
