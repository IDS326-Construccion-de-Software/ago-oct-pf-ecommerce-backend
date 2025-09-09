using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Revenge.Data.Models
{
    public class RegisterUserDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; } = null!;

        [Phone]
        [StringLength(20)]
        public string? Cellphone { get; set; }

        public DateOnly? Birthdate { get; set; }

        public object? Directions { get; set; }

        [Range(1, int.MaxValue)]
        public int? NumIdentification { get; set; }
    }
}