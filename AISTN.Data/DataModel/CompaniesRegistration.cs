using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class CompaniesRegistration
{
    public Guid Id { get; set; }

    public DateTime ImportedDate { get; set; }

    public Guid ImportRequestId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string LegalForm { get; set; } = null!;

    public string Bulstat { get; set; } = null!;

    public DateTime? FieldActionDate { get; set; }

    public int? CountryId { get; set; }

    public string? CountryCode { get; set; }

    public string? Country { get; set; }

    public bool? IsForeign { get; set; }

    public int? DistrictId { get; set; }

    public string? DistrictEkatte { get; set; }

    public string? District { get; set; }

    public int? Municipalityid { get; set; }

    public string? MunicipalityEkatte { get; set; }

    public string? Municipality { get; set; }

    public int? SettlementId { get; set; }

    public string? SettlementEkatte { get; set; }

    public string? Settlement { get; set; }

    public int? AreaId { get; set; }

    public string? Area { get; set; }

    public string AreaEkatte { get; set; } = null!;

    public string? PostCode { get; set; }

    public string? ForeignPlace { get; set; }

    public string? HousingEstate { get; set; }

    public string? Street { get; set; }

    public string? StreetNumber { get; set; }

    public string? Block { get; set; }

    public string? Entrance { get; set; }

    public string? Floor { get; set; }

    public string? Apartment { get; set; }

    public virtual ImportRequest ImportRequest { get; set; } = null!;
}
