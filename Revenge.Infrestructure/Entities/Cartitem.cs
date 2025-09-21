using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Cartitem
{
    public Guid Id { get; set; }

    public Guid CartId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime? AddedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Shoppingcart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
