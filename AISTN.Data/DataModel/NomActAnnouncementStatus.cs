using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomActAnnouncementStatus
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ActAnnouncement> ActAnnouncementAnnouncementStatuses { get; set; } = new List<ActAnnouncement>();

    public virtual ICollection<ActAnnouncement> ActAnnouncementRegistrationStatuses { get; set; } = new List<ActAnnouncement>();
}
