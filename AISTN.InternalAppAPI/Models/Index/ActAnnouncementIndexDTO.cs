namespace AISTN.InternalAppAPI.Models.Index
{
    public class ActAnnouncementIndexDTO
    {
        public Guid Id { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public string? CourtName { get; set; }

        public string? ProprietorName { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public string? AnnouncementStatus { get; set; }

        public string? RegistrationStatus { get; set; }

        public DateTime? AnnouncedDate { get; set; }

        public string? AnnounceByUserName { get; set; }
    }
}
