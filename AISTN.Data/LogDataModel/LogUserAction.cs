using System;
using System.Collections.Generic;

namespace AISTN.Data.LogDataModel;

public partial class LogUserAction
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string UserActionType { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string? Message { get; set; }

    public string? IpAddress { get; set; }

    public virtual ICollection<LogDbcontext> LogDbcontexts { get; set; } = new List<LogDbcontext>();
}
