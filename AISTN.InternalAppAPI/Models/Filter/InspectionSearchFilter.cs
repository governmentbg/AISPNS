namespace AISTN.InternalAppAPI.Models.Filter
{
    public class InspectionSearchFilter
    {
        public Guid? SyndicID { get; set; }
        public Guid? InspectionTypeID { get; set; }
        public string? InspectorName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
