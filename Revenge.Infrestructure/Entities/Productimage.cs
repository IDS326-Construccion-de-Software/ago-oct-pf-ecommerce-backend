using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Productimage
{
    public Guid id { get; set; }

    public Guid productId { get; set; }

    public string url { get; set; } = null!;

    public string? altText { get; set; }

    public bool? isPrimary { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual Product product { get; set; } = null!;
}
