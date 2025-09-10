using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Cartitem
{
    public Guid id { get; set; }

    public Guid cartId { get; set; }

    public Guid productId { get; set; }

    public int quantity { get; set; }

    public DateTime? addedAt { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual Shoppingcart cart { get; set; } = null!;

    public virtual Product product { get; set; } = null!;
}
