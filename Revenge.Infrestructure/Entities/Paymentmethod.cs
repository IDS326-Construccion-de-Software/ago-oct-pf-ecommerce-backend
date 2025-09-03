using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Paymentmethod
{
    public Guid id { get; set; }

    public string name { get; set; } = null!;

    public string? provider { get; set; }

    public string? metadata { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual ICollection<Payment> payments { get; set; } = new List<Payment>();
}
