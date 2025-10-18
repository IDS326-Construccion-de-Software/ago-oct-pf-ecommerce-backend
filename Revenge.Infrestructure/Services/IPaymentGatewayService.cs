using System.Text.Json.Serialization;

namespace Revenge.Infrestructure.Services
{
    /// <summary>
    /// Servicio para integración con el Payment Gateway de Falcon
    /// </summary>
    public interface IPaymentGatewayService
    {
        /// <summary>
        /// Procesa un pago a través del gateway externo
        /// </summary>
        Task<PaymentGatewayResponse> ProcessPaymentAsync(
            PaymentGatewayRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene el historial de transacciones del gateway
        /// </summary>
        Task<TransactionHistory[]> GetTransactionHistoryAsync(
            DateTime start,
            DateTime end,
            CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Modelo de solicitud para el Payment Gateway de Falcon
    /// </summary>
    public class PaymentGatewayRequest
    {
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; } = null!;

        [JsonPropertyName("reference")]
        public string Reference { get; set; } = null!;

        [JsonPropertyName("cc")]
        public string Cc { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("cvv")]
        public string Cvv { get; set; } = null!;

        [JsonPropertyName("expMonth")]
        public int ExpMonth { get; set; }

        [JsonPropertyName("expYear")]
        public int ExpYear { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; } = null!;
    }

    /// <summary>
    /// Modelo de respuesta del Payment Gateway de Falcon
    /// </summary>
    public class PaymentGatewayResponse
    {
        [JsonPropertyName("authCode")]
        public string? AuthCode { get; set; }

        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; } = null!;

        [JsonPropertyName("errorMsg")]
        public string? ErrorMsg { get; set; }
    }

    /// <summary>
    /// Modelo para el historial de transacciones del gateway
    /// </summary>
    public class TransactionHistory
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }

        [JsonPropertyName("authCode")]
        public string? AuthCode { get; set; }

        [JsonPropertyName("approved")]
        public bool Approved { get; set; }
    }
}
