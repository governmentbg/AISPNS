namespace AISTN.Common.Models
{
    public class SaveMessageDTO
    {
        public Guid? Id { get; set; }

        public Guid? SenderId { get; set; }

        public string? Number { get; set; }

        public string? SyndicElectronicAddress { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool SendToAll { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public DateTime? SendDate { get; set; }

        public DateTime? SeenDate { get; set; }

        public IEnumerable<Guid>? MessageReceiverIDs { get; set; }

        public SaveSenderDTO? Sender { get; set; }

        public IEnumerable<FileDTO>? Files { get; set; }
    }
}
