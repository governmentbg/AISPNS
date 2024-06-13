namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsActDTO
    {
        public Guid Id { get; set; }

        public Guid ActAnnouncementId { get; set; }

        public Guid SessionId { get; set; }

        public int? Action { get; set; }

        public string? ActKind { get; set; }

        public string? ActCategory { get; set; }

        public string? ActContractor { get; set; }

        public string? ActReason { get; set; }

        public string? Number { get; set; }

        public Guid? AnnouncementStatusId { get; set; }

        public string? AnnouncementStatus { get; set; }

        public Guid? RegistrationStatusId { get; set; }

        public string? RegistrationStatus { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public string? CourtName { get; set; }

        public string? ProprietorName { get; set; }

        public DateTime Date { get; set; }

        public short? DebtorStatus { get; set; }

        public string? Text { get; set; }

        public byte[]? Image { get; set; }

        public byte[]? OriginalLetterImage { get; set; }

        public byte[]? RedactedLetterImage { get; set; }
    }
}
