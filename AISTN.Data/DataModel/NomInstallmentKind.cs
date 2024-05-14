using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomInstallmentKind
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Installment> Installments { get; set; } = new List<Installment>();
}
