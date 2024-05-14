using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveSyndicDTO
    {
        public Guid? Id { get; set; }

        public Guid? UserId { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? SecondLastName { get; set; }

        public string? LastName { get; set; }

        public string? Egn { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public bool? Active { get; set; }

        public bool? Locked { get; set; }

        public string? Notes { get; set; }

        public bool? IsCustodian { get; set; }

        public Guid? Specialty { get; set; }

        public Guid? SyndicStatusId { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public AddressIndexDTO? Address { get; set; }

        public OrderIndexDTO? Order { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
