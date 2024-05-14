namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveAppealDTO
    {
        public Guid? Id { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? ActId { get; set; }

        public string? Number { get; set; }

        public DateTime? Date { get; set; }

        public int? Action { get; set; }

        public Guid? AppealKindId { get; set; }

        public Guid? SyndicId { get; set; }

        public Guid? DocumentCollectionId { get; set; }
    }
}
