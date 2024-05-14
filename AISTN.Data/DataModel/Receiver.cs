using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Receiver
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<MessageReceiver> MessageReceivers { get; set; } = new List<MessageReceiver>();

    public virtual User? User { get; set; }
}
