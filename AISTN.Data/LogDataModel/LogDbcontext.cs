using System;
using System.Collections.Generic;

namespace AISTN.Data.LogDataModel;

public partial class LogDbcontext
{
    public int Id { get; set; }

    public string ActionTable { get; set; } = null!;

    public string? TableRow { get; set; }

    public string ActionType { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public int? UserActionLogId { get; set; }

    public virtual LogUserAction? UserActionLog { get; set; }
}
