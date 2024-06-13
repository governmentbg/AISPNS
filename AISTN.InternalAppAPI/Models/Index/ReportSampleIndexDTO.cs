namespace AISTN.InternalAppAPI.Models.Index
{
    public class ReportSampleIndexDTO
    {
        public Guid? Id { get; set; }

        public string? ReportSampleKindDescription { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public Guid? AttachedDocumentCollectionID { get; set; }
    }
}
