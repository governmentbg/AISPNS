namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsEntityStatisticalDataDTO
    {
        public Guid? Id { get; set; }

        public Guid EntityId { get; set; }

        public int? NumberJobCuts { get; set; }

        public bool? WasRestructured { get; set; }

        public string? CompanySizeName { get; set; }
    }
}
