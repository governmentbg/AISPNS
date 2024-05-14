using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveRegisterEntryDTO
    {
        public Guid? Id { get; set; }

        public Guid ActId { get; set; }

        public Guid? ProprietorId { get; set; }

        public Guid? FieldId { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public Guid? LegalBasisId { get; set; }

        public bool? IsEnforcedImmediately { get; set; }

        public int? AppealTerm { get; set; }

        public Guid? AnnouncedByUserId { get; set; }

        public DateTime? AnnouncedDate { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Guid? ReplacedByEntryId { get; set; }

        public Guid? PreviousEntryId { get; set; }

        public Guid? SyndicId { get; set; }

        public Guid? AddressId { get; set; }

        public Guid? RegisterSyndicKindId { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Number { get; set; }

        public AddressIndexDTO? Address { get; set; }

        public SaveRegisterEntryDTO? ReplacedByEntry { get; set; }

        public SaveRegisterEntryDTO? PreviousEntry { get; set; }
    }
}
