using System;
using System.ComponentModel.DataAnnotations;

namespace Revenge.Core.Models
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public string Brand { get; set; } = null!;
    }
}
