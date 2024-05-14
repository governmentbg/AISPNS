using AISTN.Common.Models;
using AISTN.InternalAppAPI.Models.Save;

namespace AISTN.InternalAppAPI.Models
{
    public class RegisterEntryDTO
    {
        public Guid? Id { get; set; }

        public Guid ActId { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public bool? IsEnforcedImmediately { get; set; }

        public int? AppealTerm { get; set; }

        public DateTime? AnnouncedDate { get; set; }

        public string? SyndicFullName { get; set; }

        public string? FieldName { get; set; }

        public string? LegalBasisName { get; set; }

        public string? RegisterSyndicKindName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Number { get; set; }

        public string? FullAddress { get; set; }

        public SaveActDTO? Act { get; set; }

        public SaveRegisterEntryDTO? ReplacedByEntry { get; set; }

        public SaveRegisterEntryDTO? PreviousEntry { get; set; }

        public DocumentCollectionDTO? DocumentCollection { get; set; }
    }
}
