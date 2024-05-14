using DocumentFormat.OpenXml.Bibliography;

namespace AISTN.ExternalAppAPI.Models.Export
{
    public class OpenDataAnnouncementDTO
    {
        public string? CaseNumber { get; set; }

        public string? CaseYear { get; set; }

        public string? CourtName { get; set; }

        public string? OfferingDate { get; set; }

        public string? Address { get; set; }

        public string? SyndicName { get; set; }

        public string? DebtorName { get; set; }

        public string? OfferingKind { get; set; }

        public string? ObjectType { get; set; }

        public string? URL { get; set; }
    }
}
