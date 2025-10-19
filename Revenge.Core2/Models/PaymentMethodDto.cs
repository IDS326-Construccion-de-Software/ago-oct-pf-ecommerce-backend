using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenge.Core.Models
{
    internal class PaymentMethodDto
    {
        public Guid Id { get; set; }
        public string Method { get; set; } = null!;
        public string? Description { get; set; }
    }
}
