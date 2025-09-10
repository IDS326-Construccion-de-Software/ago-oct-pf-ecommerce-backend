using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public Guid? Addressid { get; set; }

    public decimal Total { get; set; }

    public DateTime? Placedat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
