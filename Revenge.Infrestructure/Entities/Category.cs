using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Category
{
    public Guid id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    public DateTime? createdAt { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual ICollection<Product> products { get; set; } = new List<Product>();
}
