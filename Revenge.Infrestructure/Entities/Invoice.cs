using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Invoice
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public DateTime? IssuedAt { get; set; }

    public decimal Total { get; set; }

    public decimal Tax { get; set; }

    public string Url { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
