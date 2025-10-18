using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenge.Data.Models
{
    public class OrderItemDTO
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Información básica del producto
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string? ProductBrand { get; set; }
    }
}