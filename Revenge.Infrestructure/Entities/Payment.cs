using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Payment
{
    public Guid id { get; set; }

    public Guid userId { get; set; }

    public Guid? orderId { get; set; }

    public Guid? invoiceId { get; set; }

    public Guid paymentMethodId { get; set; }

    public decimal amount { get; set; }

    public string? transactionReference { get; set; }

    public DateTime? paidAt { get; set; }

    public DateTime? createdAt { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual Invoice? invoice { get; set; }

    public virtual Order? order { get; set; }

    public virtual Paymentmethod paymentMethod { get; set; } = null!;

    public virtual User user { get; set; } = null!;
}
