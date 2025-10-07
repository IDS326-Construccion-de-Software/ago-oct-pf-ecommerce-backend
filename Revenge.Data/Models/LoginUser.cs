using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Revenge.Data.Models
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; } = null!;
    }
}
