using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class ImportRequest
{
    public Guid Id { get; set; }

    public DateTime ImportDate { get; set; }

    public string Filename { get; set; } = null!;

    public string Checksum { get; set; } = null!;

    public int Status { get; set; }

    public virtual ICollection<CompaniesRegistration> CompaniesRegistrations { get; set; } = new List<CompaniesRegistration>();

    public virtual ICollection<ImportedDeed> ImportedDeeds { get; set; } = new List<ImportedDeed>();
}
