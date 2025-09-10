using System;
using System.Collections.Generic;

namespace Revenge.Infrestructure.Entities;

public partial class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
