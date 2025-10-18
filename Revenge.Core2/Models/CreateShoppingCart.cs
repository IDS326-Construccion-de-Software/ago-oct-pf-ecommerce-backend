using System;
using System.ComponentModel.DataAnnotations;

namespace Revenge.Core.Models
{
    public class CreateShoppingCartDTO
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
