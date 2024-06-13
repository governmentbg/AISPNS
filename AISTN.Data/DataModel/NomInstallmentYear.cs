using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomInstallmentYear
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public int? Year { get; set; }

    public virtual ICollection<Installment> Installments { get; set; } = new List<Installment>();
}
