using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class User
{
    public Guid id { get; set; }

    public string name { get; set; } = null!;

    public string email { get; set; } = null!;

    public string password { get; set; } = null!;

    public string? cellphone { get; set; }

    public DateOnly? birthdate { get; set; }

    public string? directions { get; set; }

    public DateTime? createdAt { get; set; }

    public DateTime? updatedAt { get; set; }

    public int? numIdentification { get; set; }

    public virtual ICollection<Invoice> invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Order> orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> payments { get; set; } = new List<Payment>();

    public virtual ICollection<Shoppingcart> shoppingCarts { get; set; } = new List<Shoppingcart>();
}
