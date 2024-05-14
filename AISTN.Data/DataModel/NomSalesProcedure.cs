using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSalesProcedure
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
}
