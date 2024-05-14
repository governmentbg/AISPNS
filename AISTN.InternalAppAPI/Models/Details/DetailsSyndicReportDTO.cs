namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsSyndicReportDTO
    {
        public Guid? Id { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public string? CourtName { get; set; }

        public DateTime? reportDate { get; set; }

        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }

        public string? Notes { get; set; }

        public string? ReportTypeName { get; set; }

        public string? SyndicName { get; set; }

        public string? DebtorName { get; set; }

        public string? DebtorBulstat { get; set; }

        public Guid? DocumentCollectionId { get; set; }
    }
}
