using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Revenge.Data.Models
{
    public class RegisterUserDTO
    {

        public string Name { get; set; } = null!;


        public string Email { get; set; } = null!;


        public string Password { get; set; } = null!;


        public string? Cellphone { get; set; }

        public DateTime? Birthdate { get; set; }

        public object? Directions { get; set; }

        public int? NumIdentification { get; set; }
    }
}