using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public Guid? Orderid { get; set; }

    public Guid? Invoiceid { get; set; }

    public Guid Paymentmethodid { get; set; }

    public decimal Amount { get; set; }

    public string? Transactionreference { get; set; }

    public DateTime? Paidat { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Paymentmethod Paymentmethod { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
