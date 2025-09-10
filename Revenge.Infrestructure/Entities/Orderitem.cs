using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Orderitem
{
    public Guid Id { get; set; }

    public Guid Orderid { get; set; }

    public Guid Productid { get; set; }

    public decimal Unitprice { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
