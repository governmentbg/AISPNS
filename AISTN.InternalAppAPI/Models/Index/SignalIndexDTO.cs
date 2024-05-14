namespace AISTN.InternalAppAPI.Models.Index
{
    public class SignalIndexDTO
    {
        public Guid? Id { get; set; }

        public string? Number { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public string? CaseStatCode { get; set; }

        public string? CaseNumber { get; set; }

        public short CaseYear { get; set; }

        public string? CourtName { get; set; }

        public string? ReceiverName{ get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public Guid? SyndicId { get; set; }

        public string? SenderName { get; set; }

        public string? SenderCitizenshipNumber { get; set; }

        public string? SenderEmail { get; set; }

        public string? SenderPhone { get; set; }

        public string? SenderAddress { get; set; }

        public string? SenderType { get; set; }
    }
}
