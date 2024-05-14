namespace AISTN.Common.Models
{
    public class SendEmailDTO
    {
        public Guid SyndicId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
