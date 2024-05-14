using AISTN.Common.Models;

namespace AISTN.ExternalAppAPI.Models.Index
{
    public class ReportIndexDTO
    {
        public Guid Id { get; set; }

        public string? ReportTypeName { get; set; }

        public DateTime ReportDate { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string? Notes { get; set; }

        public string? CourtName { get; set; } 

        public string? CaseNumber { get; set; }

        public string? CaseYear { get; set; }

        public string? SyndicName { get; set; }

        public string? DebtorName { get; set; }

        public Guid? DocumentContentID { get; set; }
    }
}
