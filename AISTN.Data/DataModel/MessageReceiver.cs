using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class MessageReceiver
{
    public Guid Id { get; set; }

    public Guid MessageId { get; set; }

    public Guid ReceiverId { get; set; }

    public bool Seen { get; set; }

    public DateTime? SeenDate { get; set; }

    public virtual Message Message { get; set; } = null!;

    public virtual Receiver Receiver { get; set; } = null!;
}
