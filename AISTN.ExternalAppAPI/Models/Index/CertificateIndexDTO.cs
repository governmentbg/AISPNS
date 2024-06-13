namespace AISTN.ExternalAppAPI.Models.Index
{
    public class CertificateIndexDTO
    {
        public Guid? AnnouncementId { get; set; }

        public string? AnnouncementNumber { get; set; }

        public DateTime? PublishDate { get; set; }

        public string? DebtorName { get; set; }

        public string? SyndicName { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public string? CourtName { get; set; }
    }
}
