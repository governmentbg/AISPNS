namespace AISTN.InternalAppAPI.Models.Filter
{
    public class ActAnnouncementSearchFilter
    {
        public string? ProprietorName { get; set; }

        public string? ProprietorIdentifier { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public int? CourtNumber { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public Guid? AnnouncementStatusId { get; set; }

        public Guid? RegistrationStatusId { get; set; }

        public bool? IsStabilization { get; set; }
    }
}
