using System;
using System.Collections.Generic;

namespace AISTN.Data.LogDataModel;

public partial class LogException
{
    public long Id { get; set; }

    public string Message { get; set; } = null!;

    public string? StackTrace { get; set; }

    public string Type { get; set; } = null!;

    public string? IpAddress { get; set; }

    public string? UserId { get; set; }

    public DateTime Timestamp { get; set; }
}
