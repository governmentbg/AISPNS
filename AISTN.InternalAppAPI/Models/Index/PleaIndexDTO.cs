namespace AISTN.InternalAppAPI.Models.Index
{
    public class PleaIndexDTO
    {
        public Guid? Id { get; set; }

        public string? OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? PleaNumber { get; set; }

        public DateTime? PleaDate { get; set; }

        public string? CourtDecision { get; set; }

        public Guid? DocumentCollectionId {  get; set; }
    }
}
