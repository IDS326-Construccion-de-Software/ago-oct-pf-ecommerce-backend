using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Invoiceitem
{
    public Guid id { get; set; }

    public Guid invoiceId { get; set; }

    public Guid productId { get; set; }

    public string? description { get; set; }

    public decimal unitPrice { get; set; }

    public int quantity { get; set; }

    public decimal subtotal { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual Invoice invoice { get; set; } = null!;

    public virtual Product product { get; set; } = null!;
}
