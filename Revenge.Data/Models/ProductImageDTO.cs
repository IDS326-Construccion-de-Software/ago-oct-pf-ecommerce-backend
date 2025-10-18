using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenge.Data.Models
{
    public class ProductImageDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Url { get; set; } = null!;
        public bool IsPrimary { get; set; }
        public int Order { get; set; }

        // Información básica del producto
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string? ProductBrand { get; set; }
    }
}