using System;
using System.ComponentModel.DataAnnotations;

namespace Revenge.Core.Models
{
    public class CreateProductImageDTO
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [Url]
        public string Url { get; set; } = null!;

        public bool IsPrimary { get; set; } = false;

        public int Order { get; set; } = 0;
    }
}
