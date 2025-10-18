using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Revenge.Core.Models
{
    public class CreateInvoiceDTO
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Required]
        [Url]
        public string Url { get; set; } = null!;
    }
}