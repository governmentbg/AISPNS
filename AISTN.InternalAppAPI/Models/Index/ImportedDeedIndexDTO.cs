namespace AISTN.InternalAppAPI.Models.Index
{
    public class ImportedDeedIndexDTO
    {
        public Guid Id { get; set; }

        public Guid ImportRequestId { get; set; }

        public string? DeedRawXml { get; set; }

        public string? CompanyName { get; set; }

        public string? CompanyLegalForm { get; set; }

        public string? CompanyUic { get; set; }

        public string? FieldEntryNumber { get; set; }

        public DateTime? FieldActionDate { get; set; }

        public DateTime? FieldEntryDate { get; set; }

        public string? CaseNumber { get; set; }

        public int? CaseYear { get; set; }

        public DateTime? ActDate { get; set; }

        public string? ActNumber { get; set; }

        public string? ActType { get; set; }

        public int? ActComplaintTerm { get; set; }

        public int? CourtNo { get; set; }

        public string? CourtName { get; set; }

        public string? DeedGuid { get; set; }

        public IEnumerable<TrusteeIndexDTO>? Trustees { get; set; }
    }
}
