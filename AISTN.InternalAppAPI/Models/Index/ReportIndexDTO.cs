namespace AISTN.InternalAppAPI.Models.Index
{
    public class ReportIndexDTO
    {
        public Guid? Id { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public string? CourtName { get; set; }

        public DateTime? ReportDate { get; set; }

        public string? Notes { get; set; }

        public string? ReportTypeName { get; set; }

        public string? SyndicName { get; set; }

        public Guid? DocumentCollectionId { get; set; }
    }
}
