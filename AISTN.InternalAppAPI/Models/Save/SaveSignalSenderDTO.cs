namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveSignalSenderDTO
    {
        public string? Name { get; set; }

        public string? CitizenshipNumber { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public Guid? SignalSenderTypeId { get; set; }

        public SaveAddressDTO? Address { get; set; }
    }
}
