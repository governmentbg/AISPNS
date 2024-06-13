using AISTN.Common.Models;
using AISTN.Data.DataModel;

namespace AISTN.PublicAppAPI.Models.Index
{
    public class AnnouncementIndexDTO
    {
        public Guid Id { get; set; }

        public DateTime OfferingDate { get; set; }

        public DateTime? PublishDate { get; set; }

        public string? DebtorName { get; set; }

        public string? FullAddress { get; set; }

        public string? SyndicName { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public string? CourtName { get; set; }

        public string? Status { get; set; }
    }
}
