using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Product
{
    public Guid id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    public decimal price { get; set; }

    public Guid categoryId { get; set; }

    public string? brand { get; set; }

    public string? url { get; set; }

    public DateTime? createdAt { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual ICollection<Cartitem> cartItems { get; set; } = new List<Cartitem>();

    public virtual Category category { get; set; } = null!;

    public virtual ICollection<Invoiceitem> invoiceItems { get; set; } = new List<Invoiceitem>();

    public virtual ICollection<Orderitem> orderItems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Productimage> productImages { get; set; } = new List<Productimage>();
}
