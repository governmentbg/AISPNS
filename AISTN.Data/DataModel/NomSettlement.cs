using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomSettlement
{
    public Guid Id { get; set; }

    public Guid? MunicipalityId { get; set; }

    public string LauCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string FullPathName { get; set; } = null!;

    public string FullPath { get; set; } = null!;

    public decimal Order { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? LastModified { get; set; }

    public int? ViewOrder { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual NomMunicipality? Municipality { get; set; }
}
