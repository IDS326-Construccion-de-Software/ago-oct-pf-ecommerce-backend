using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenge.Core.Models
{
    public class ProductImageDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Url { get; set; } = null!;
        public bool IsPrimary { get; set; }
        public int Order { get; set; }
    }
}