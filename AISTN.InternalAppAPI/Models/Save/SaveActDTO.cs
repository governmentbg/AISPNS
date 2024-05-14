using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveActDTO
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public int? Action { get; set; }

        public Guid ActKindId { get; set; }

        public Guid? ActCategoryId { get; set; }

        public Guid? ActContractorId { get; set; }

        public string? Number { get; set; }

        public DateTime Date { get; set; }

        public Guid? ActReasonId { get; set; }

        public short? DebtorStatus { get; set; }

        public string? Text { get; set; }

        public NomActCategoryDTO? ActCategory { get; set; }

        public NomActContractorDTO? ActContractor { get; set; }

        public NomActKindDTO? ActKind { get; set; }

        public NomActReasonDTO? ActReason { get; set; }

        public SessionDTO? Session { get; set; }
    }
}
