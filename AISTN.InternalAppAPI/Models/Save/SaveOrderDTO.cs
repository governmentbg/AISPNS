using AISTN.Common.Models;
using AISTN.InternalAppAPI.Models.Index;

namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveOrderDTO
    {
        public Guid? Id { get; set; }

        public Guid? OrderKindId { get; set; }

        public string? Number { get; set; }

        public DateTime Date { get; set; }

        public string? StateGazetteEdition { get; set; }

        public string? StateGazetteYear { get; set; }

        public string? Description { get; set; }

        public Guid? SyndicId { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public string? ExclusionGrounds { get; set; }

        public DateTime? ExclusionStartDate { get; set; }

        public DateTime? ExclusionEndDate { get; set; }

        public DateTime? ExclusionTemporaryDate { get; set; }

        public bool IsExclusion { get; set; }
    }
}
