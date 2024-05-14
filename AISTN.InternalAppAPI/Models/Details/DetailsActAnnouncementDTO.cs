namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsActAnnouncementDTO
    {
        public Guid? Id { get; set; }

        public string? ActCategory { get; set; }

        public Guid? ActId { get; set; }

        public string? Description { get; set; }

        public DateTime? AnnouncedDate { get; set; }

        public Guid? AnnouncedByUserId { get; set; }

        public bool? IsEnforcedImmediately { get; set; }

        public int? AppealTerm { get; set; }

        public string? AnnouncementStatus { get; set; }

        public string? RegistrationStatus { get; set; }

        public Guid? DocumentCollectionId { get; set; }
    }
}
