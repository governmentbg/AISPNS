using AISTN.Common.Models;

namespace AISTN.PublicAppAPI.Models.Details
{
    public class DetailsSyndicDTO
    {
        public string FullName { get; set; }

        public string? FullAddress { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? SyndicSpecialty { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? OrderNumber { get; set; }

        public string? StateGazetteEdition { get; set; }

        public string? StateGazetteYear { get; set; }
    }
}
