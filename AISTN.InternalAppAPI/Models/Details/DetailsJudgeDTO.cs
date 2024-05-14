namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsJudgeDTO
    {
        public Guid? Id { get; set; }

        public Guid? SessionId { get; set; }

        public Guid? CaseId { get; set; }

        public string? Name { get; set; }

        public string? Rename { get; set; }

        public string? Family { get; set; }

        public string? Egn { get; set; }
    }
}
