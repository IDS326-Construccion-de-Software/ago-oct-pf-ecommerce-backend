using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Productimage
{
    public Guid Id { get; set; }

    public Guid Productid { get; set; }

    public string Url { get; set; } = null!;

    public string? Alttext { get; set; }

    public bool? Isprimary { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Product Product { get; set; } = null!;
}
