using AISTN.Common.Models;

namespace AISTN.PublicAppAPI.Models.Details
{
    public class DetailsStatisticalReportDTO
    {
        public Guid Id { get; set; }

        public string? IdentificationNumbr { get; set; }

        public string? Type { get; set; }

        public string? Source { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? Published { get; set; }

        public DocumentCollectionDTO? DocumentCollection { get; set; }
    }
}
