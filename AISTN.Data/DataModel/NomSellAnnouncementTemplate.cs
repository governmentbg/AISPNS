using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSellAnnouncementTemplate
{
    public Guid Id { get; set; }

    public int? Number { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
