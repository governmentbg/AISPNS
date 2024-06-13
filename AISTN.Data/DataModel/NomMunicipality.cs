using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomMunicipality
{
    public Guid Id { get; set; }

    public Guid? DistrictId { get; set; }

    public string LauCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string FullPathName { get; set; } = null!;

    public string FullPath { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? LastModified { get; set; }

    public int? ViewOrder { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual NomDistrict? District { get; set; }

    public virtual ICollection<NomSettlement> NomSettlements { get; set; } = new List<NomSettlement>();
}
