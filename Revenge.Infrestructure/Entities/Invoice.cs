using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Invoice
{
    public Guid Id { get; set; }

    public Guid Orderid { get; set; }

    public Guid Userid { get; set; }

    public DateTime? Issuedat { get; set; }

    public decimal Total { get; set; }

    public decimal? Tax { get; set; }

    public string? Notes { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Invoiceitem> Invoiceitems { get; set; } = new List<Invoiceitem>();

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
