namespace AISTN.InternalAppAPI.Models.Index
{
    public class RegisterEntryIndexDTO
    {
        public Guid Id { get; set; }

        public string? ActKind { get; set; }

        public string? ActNumber { get; set; }

        public string? Number { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public bool? IsEnforcedImmediately { get; set; }

        public DateTime? AnnouncedDate { get; set; }

        public DateTime? DateCreated { get; set; }

        public string? LegalBasisName { get; set; }

        public string? FieldName { get; set; }

        public string? AppealTerm { get; set; }

        public string? AnnouncedByUser { get; set; }

        public Guid? DocumentCollectionId { get; set; }
    }
}
