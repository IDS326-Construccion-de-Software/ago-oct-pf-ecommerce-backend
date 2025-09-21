using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? OrderId { get; set; }

    public Guid? InvoiceId { get; set; }

    public Guid PaymentMethodId { get; set; }

    public decimal Amount { get; set; }

    public string? TransactionReference { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Paymentmethod PaymentMethod { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
