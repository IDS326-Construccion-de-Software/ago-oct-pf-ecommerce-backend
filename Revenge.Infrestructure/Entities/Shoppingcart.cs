using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Shoppingcart
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    public virtual User User { get; set; } = null!;
}
