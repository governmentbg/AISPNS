using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class ActDetail
{
    public Guid Id { get; set; }

    public Guid DeedId { get; set; }

    public DateTime ImportedDate { get; set; }

    public string? Title { get; set; }

    public string? Date { get; set; }

    public DateTime? ActDate { get; set; }

    public string? ActContainerType { get; set; }

    public int? RecordId { get; set; }

    public virtual ImportedDeed Deed { get; set; } = null!;
}
