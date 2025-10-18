using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenge.Core.Models
{
    internal class InvoiceDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CartId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
