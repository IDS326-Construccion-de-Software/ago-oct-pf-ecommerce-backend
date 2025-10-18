using System.ComponentModel.DataAnnotations;

namespace Revenge.Data.Models
{
    /// <summary>
    /// DTO para procesar solicitudes de pago
    /// </summary>
    public class PaymentRequestDTO
    {
        [Required(ErrorMessage = "El ID del usuario es requerido")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El ID de la orden es requerido")]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "El ID de la factura es requerido")]
        public Guid InvoiceId { get; set; }

        [Required(ErrorMessage = "El número de tarjeta es requerido")]
        [CreditCard(ErrorMessage = "El número de tarjeta no es válido")]
        [StringLength(19, MinimumLength = 13, ErrorMessage = "El número de tarjeta debe tener entre 13 y 19 dígitos")]
        public string CreditCardNumber { get; set; } = null!;

        [Required(ErrorMessage = "El nombre del titular es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
        public string CardHolderName { get; set; } = null!;

        [Required(ErrorMessage = "El CVV es requerido")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "El CVV debe tener 3 o 4 dígitos")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "El CVV debe contener solo números")]
        public string Cvv { get; set; } = null!;

        [Required(ErrorMessage = "El mes de expiración es requerido")]
        [Range(1, 12, ErrorMessage = "El mes debe estar entre 1 y 12")]
        public int ExpirationMonth { get; set; }

        [Required(ErrorMessage = "El año de expiración es requerido")]
        [Range(0, 99, ErrorMessage = "El año debe estar entre 00 y 99")]
        public int ExpirationYear { get; set; }

        [Required(ErrorMessage = "El monto es requerido")]
        [Range(0.01, 999999.99, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "El impuesto es requerido")]
        [Range(0, 999999.99, ErrorMessage = "El impuesto debe ser mayor o igual a 0")]
        public decimal Tax { get; set; }

        /// <summary>
        /// Valida que la tarjeta no esté vencida
        /// </summary>
        public bool IsCardExpired()
        {
            var now = DateTime.UtcNow;
            var currentYear = now.Year % 100; // Últimos 2 dígitos
            var currentMonth = now.Month;

            if (ExpirationYear < currentYear)
                return true;

            if (ExpirationYear == currentYear && ExpirationMonth < currentMonth)
                return true;

            return false;
        }
    }
}
