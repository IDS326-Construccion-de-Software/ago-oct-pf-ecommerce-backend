using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenge.Core.Models
{
    internal class InvoiceDTO
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public DateTime? IssuedAt { get; set; }

        public decimal Total { get; set; }

        public decimal Tax { get; set; }

        public string Url { get; set; } = null!;
    }
}
