using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Cartitem
{
    public Guid Id { get; set; }

    public Guid Cartid { get; set; }

    public Guid Productid { get; set; }

    public int Quantity { get; set; }

    public DateTime? Addedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Shoppingcart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
