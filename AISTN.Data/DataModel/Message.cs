using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Message
{
    public Guid Id { get; set; }

    public Guid SenderId { get; set; }

    public int Number { get; set; }

    public string? SyndicElectronicAddress { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public Guid? ParentId { get; set; }

    public Guid? ReplyId { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual ICollection<Message> InverseParent { get; set; } = new List<Message>();

    public virtual ICollection<Message> InverseReply { get; set; } = new List<Message>();

    public virtual ICollection<MessageReceiver> MessageReceivers { get; set; } = new List<MessageReceiver>();

    public virtual Message? Parent { get; set; }

    public virtual Message? Reply { get; set; }

    public virtual Sender Sender { get; set; } = null!;
}
