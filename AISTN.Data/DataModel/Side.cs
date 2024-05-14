using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Side
{
    public Guid Id { get; set; }

    public Guid? CaseId { get; set; }

    public Guid? SurroundDocumentId { get; set; }

    public Guid? AppealId { get; set; }

    public Guid InvolvementKindId { get; set; }

    public Guid? PersonId { get; set; }

    public Guid? EntityId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public Guid? DebtorStatusId { get; set; }

    public bool IsDebtor { get; set; }

    public virtual Appeal? Appeal { get; set; }

    public virtual Case? Case { get; set; }

    public virtual NomDebtorStatus? DebtorStatus { get; set; }

    public virtual Entity? Entity { get; set; }

    public virtual NomInvolvementKind InvolvementKind { get; set; } = null!;

    public virtual Person? Person { get; set; }

    public virtual ICollection<RegisterEntry> RegisterEntries { get; set; } = new List<RegisterEntry>();

    public virtual SurroundDocument? SurroundDocument { get; set; }
}
