using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class TempSession
{
    public string? Id { get; set; }

    public string? Caseid { get; set; }

    public Guid? Action { get; set; }

    public Guid? Kind { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? Time { get; set; }

    public Guid? Sessionresult { get; set; }

    public string? Text { get; set; }
}
