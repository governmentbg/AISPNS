namespace AISTN.InternalAppAPI.Models
{
    public class SettingDTO
    {
        public Guid? Id { get; set; }

        public string? MinisterName { get; set; }

        public string? ChiefInspectorDescription { get; set; }

        public string? ChiefInspector { get; set; }

        public string? PaymentInstruction { get; set; }

        public string? BankName { get; set; }

        public string? Bic { get; set; }

        public string? Iban { get; set; }

        public int? NotificationDeadline { get; set; }
    }
}
