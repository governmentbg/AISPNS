namespace AISTN.InternalAppAPI.Models
{
    public class GroupedActAnnouncements
    {
        public Guid Category { get; set; }
        public List<ActAnnouncementDTO>? ActAnnouncements { get; set; }
    }
}
