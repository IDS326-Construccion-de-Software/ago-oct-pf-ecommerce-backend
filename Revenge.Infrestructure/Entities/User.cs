using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Cellphone { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? Directions { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? NumIdentification { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Shoppingcart> Shoppingcarts { get; set; } = new List<Shoppingcart>();
}
