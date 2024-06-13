namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveInspectionDTO
    {
        public Guid? Id { get; set; }
        public DateTime InspectionDate { get; set; }

        public Guid SyndicId { get; set; }

        public string? InspectorName { get; set; }

        public Guid InspectionTypeId { get; set; }

        public string? InspectionOrderNumber { get; set; }

        public DateTime? InspectionOrderDate { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? CompletionTime { get; set; }
    }
}
