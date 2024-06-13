using System;
using System.Collections.Generic;

namespace AISTN.Data.LogDataModel;

public partial class LogApiRequest
{
    public long Id { get; set; }

    public string Endpoint { get; set; } = null!;

    public string RequestContent { get; set; } = null!;

    public string? ResponseContent { get; set; }

    public int? ResponseHttpCode { get; set; }

    public string IpAddress { get; set; } = null!;

    public DateTime RequestTimestamp { get; set; }

    public DateTime? ResponseTimestamp { get; set; }

    public int? ProcessingStatus { get; set; }

    public long? ExceptionId { get; set; }
}
