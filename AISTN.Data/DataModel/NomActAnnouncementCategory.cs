using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomActAnnouncementCategory
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public bool? IsStabilization { get; set; }

    public virtual ICollection<ActAnnouncement> ActAnnouncements { get; set; } = new List<ActAnnouncement>();
}
