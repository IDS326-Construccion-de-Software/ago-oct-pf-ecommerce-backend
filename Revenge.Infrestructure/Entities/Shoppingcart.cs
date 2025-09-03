using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Shoppingcart
{
    public Guid id { get; set; }

    public Guid userId { get; set; }

    public DateTime? createdAt { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual ICollection<Cartitem> cartItems { get; set; } = new List<Cartitem>();

    public virtual User user { get; set; } = null!;
}
