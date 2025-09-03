using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Orderitem
{
    public Guid id { get; set; }

    public Guid orderId { get; set; }

    public Guid productId { get; set; }

    public decimal unitPrice { get; set; }

    public int quantity { get; set; }

    public decimal subtotal { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual Order order { get; set; } = null!;

    public virtual Product product { get; set; } = null!;
}
