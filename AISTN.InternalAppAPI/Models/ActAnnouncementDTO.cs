using AISTN.Common.Models;
using AISTN.InternalAppAPI.Models.Save;

namespace AISTN.InternalAppAPI.Models
{
    public class ActAnnouncementDTO
    {
        public Guid? Id { get; set; }

        public Guid? ActCategoryId { get; set; }

        public Guid? ActId { get; set; }

        public Guid? AnnouncementStatusId { get; set; }

        public Guid? RegistrationStatusId { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public string? Description { get; set; }

        public DateTime? AnnouncedDate { get; set; }

        public Guid? AnnouncedByUserId { get; set; }

        public bool? IsEnforcedImmediately { get; set; }

        public int? AppealTerm { get; set; }

        public string? ActCategoryname { get; set; }

        public string? AnnouncedByUserName { get; set; }

        public string? Number { get; set; }

        public DateTime? Date { get; set; }

        public SaveAccountUserDTO? AnnouncedByUser { get; set; }

        public SaveActDTO? Act { get; set; }

        public DocumentCollectionDTO? DocumentCollection { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
