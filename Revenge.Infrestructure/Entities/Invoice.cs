using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Invoice
{
    public Guid id { get; set; }

    public Guid orderId { get; set; }

    public Guid userId { get; set; }

    public DateTime? issuedAt { get; set; }

    public decimal total { get; set; }

    public decimal? tax { get; set; }

    public string? notes { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual ICollection<Invoiceitem> invoiceItems { get; set; } = new List<Invoiceitem>();

    public virtual Order order { get; set; } = null!;

    public virtual ICollection<Payment> payments { get; set; } = new List<Payment>();

    public virtual User user { get; set; } = null!;
}
