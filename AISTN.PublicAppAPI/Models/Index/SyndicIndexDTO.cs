using AISTN.Common.Models;

namespace AISTN.PublicAppAPI.Models.Index
{
    public class SyndicIndexDTO
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string? SyndicSpecialty { get; set; }

        public string? Phone { get; set; }

        public string? FullAddress { get; set; }
    }
}
