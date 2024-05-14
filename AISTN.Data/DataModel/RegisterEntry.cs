using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class RegisterEntry
{
    public Guid Id { get; set; }

    public Guid ActId { get; set; }

    public Guid ProprietorId { get; set; }

    public Guid? FieldId { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public Guid? LegalBasisId { get; set; }

    public bool? IsEnforcedImmediately { get; set; }

    public int? AppealTerm { get; set; }

    public Guid? AnnouncedByUserId { get; set; }

    public DateTime? AnnouncedDate { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public Guid? ReplacedByEntryId { get; set; }

    public Guid? PreviousEntryId { get; set; }

    public Guid? SyndicId { get; set; }

    public Guid? RegisterSyndicKindId { get; set; }

    public Guid? AddressId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Number { get; set; }

    public virtual Act Act { get; set; } = null!;

    public virtual Address? Address { get; set; }

    public virtual User? AnnouncedByUser { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual NomRegisterField? Field { get; set; }

    public virtual ICollection<RegisterEntry> InversePreviousEntry { get; set; } = new List<RegisterEntry>();

    public virtual ICollection<RegisterEntry> InverseReplacedByEntry { get; set; } = new List<RegisterEntry>();

    public virtual NomRegistrationLegalBasis? LegalBasis { get; set; }

    public virtual RegisterEntry? PreviousEntry { get; set; }

    public virtual Side Proprietor { get; set; } = null!;

    public virtual NomRegisterSyndicKind? RegisterSyndicKind { get; set; }

    public virtual RegisterEntry? ReplacedByEntry { get; set; }

    public virtual Syndic? Syndic { get; set; }
}
