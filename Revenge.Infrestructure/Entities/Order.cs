using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Order
{
    public Guid id { get; set; }

    public Guid userId { get; set; }

    public Guid? addressId { get; set; }

    public decimal total { get; set; }

    public DateTime? placedAt { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual ICollection<Invoice> invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Orderitem> orderItems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Payment> payments { get; set; } = new List<Payment>();

    public virtual User user { get; set; } = null!;
}
