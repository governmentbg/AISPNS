using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Sender
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Email { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User? User { get; set; }
}
