using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Productimage
{
    /// <summary>
    /// productId
    /// </summary>
    public Guid ProductId { get; set; }

    public Guid Id { get; set; }

    public string Url { get; set; } = null!;

    public bool IsPrimary { get; set; }

    public int Order { get; set; }

    public virtual Product Product { get; set; } = null!;
}
