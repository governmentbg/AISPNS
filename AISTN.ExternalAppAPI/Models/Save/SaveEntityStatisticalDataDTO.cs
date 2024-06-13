using AISTN.Data.DataModel;

namespace AISTN.ExternalAppAPI.Models.Save
{
    public class SaveEntityStatisticalDataDTO
    {
        public Guid? Id { get; set; }

        public Guid EntityId { get; set; }

        public int? NumberJobCuts { get; set; }

        public bool? WasRestructured { get; set; }

        public Guid? CompanySizeId { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
