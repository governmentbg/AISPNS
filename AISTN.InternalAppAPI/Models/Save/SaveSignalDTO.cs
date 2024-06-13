namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveSignalDTO
    {
        public Guid? Id { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public Guid CaseId { get; set; }

        public Guid SyndicId { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public SaveSignalSenderDTO? Sender { get; set; }
    }
}
