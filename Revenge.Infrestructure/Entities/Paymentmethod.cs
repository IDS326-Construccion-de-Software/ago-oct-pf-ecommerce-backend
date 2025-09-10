using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Paymentmethod
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Provider { get; set; }

    public string? Metadata { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
