using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Session1
{
    public Guid Id { get; set; }

    public Guid CaseId { get; set; }

    public Guid? ActionId { get; set; }

    public Guid? KindId { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? Time { get; set; }

    public Guid? ResultId { get; set; }

    public string? Text { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}
