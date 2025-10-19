namespace Revenge.Data.Models
{
    /// <summary>
    /// DTO para respuestas de procesamiento de pago
    /// </summary>
    public class PaymentResponseDTO
    {
        /// <summary>
        /// Código de autorización del gateway (si el pago fue aprobado)
        /// </summary>
        public string? AuthCode { get; set; }

        /// <summary>
        /// Código de respuesta del gateway
        /// "00" o "0" indica éxito
        /// </summary>
        public string ResponseCode { get; set; } = null!;

        /// <summary>
        /// Mensaje de error del gateway (si aplica)
        /// </summary>
        public string? ErrorMsg { get; set; }

        /// <summary>
        /// Indica si el pago fue procesado exitosamente
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// ID del registro de pago en la base de datos
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Referencia de la transacción
        /// </summary>
        public string TransactionReference { get; set; } = null!;

        /// <summary>
        /// Fecha y hora en que se procesó el pago
        /// </summary>
        public DateTime ProcessedAt { get; set; }

        /// <summary>
        /// Monto procesado
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Estado del pago: "pending", "completed", "failed"
        /// </summary>
        public string PaymentStatus { get; set; } = null!;
    }
}
