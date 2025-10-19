using System.Security.Cryptography;
using System.Text;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Revenge.Infrestructure.Services;

namespace Revenge.Data.Services
{
    /// <summary>
    /// Servicio para integración con el Payment Gateway de Falcon.
    /// Implementa la interfaz IPaymentGatewayService
    /// y maneja la comunicación con el gateway externo.
    /// </summary>
    public class PaymentGatewayService : IPaymentGatewayService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PaymentGatewayService> _logger;
        private readonly string _apiKey;
        private readonly string _secret;
        private readonly string _baseUrl;

        public PaymentGatewayService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<PaymentGatewayService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _apiKey = configuration["PaymentGateway:ApiKey"]
                ?? throw new InvalidOperationException("PaymentGateway:ApiKey no está configurado");
            _secret = configuration["PaymentGateway:Secret"]
                ?? throw new InvalidOperationException("PaymentGateway:Secret no está configurado");
            _baseUrl = configuration["PaymentGateway:BaseUrl"]
                ?? "https://cctest.falcon.com.do/";
        }

        /// <summary>
        /// Procesa un pago a través del gateway externo de Falcon.
        /// </summary>
        public async Task<PaymentGatewayResponse> ProcessPaymentAsync(
            PaymentGatewayRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando procesamiento de pago. Reference: {Reference}",
                    request.Reference);

                // Asignar el ApiKey de la configuración al request
                request.ApiKey = _apiKey;

                // Generar el hash de seguridad HMAC SHA-512
                request.Hash = GenerateHash(request);

                _logger.LogDebug(
                    "Enviando request al gateway. Reference: {Reference}, Amount: {Amount}",
                    request.Reference,
                    request.Amount);

                // Realizar la llamada POST al gateway
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_baseUrl}Payment",
                    request,
                    cancellationToken);

                // Verificar status code HTTP
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogError(
                        "Gateway retornó error HTTP {StatusCode}. Content: {Content}",
                        response.StatusCode,
                        errorContent);

                    throw new HttpRequestException(
                        $"Gateway retornó status code {response.StatusCode}");
                }

                // Deserializar la respuesta
                var result = await response.Content.ReadFromJsonAsync<PaymentGatewayResponse>(
                    cancellationToken: cancellationToken);

                if (result == null)
                {
                    _logger.LogError("Gateway retornó respuesta vacía");
                    throw new InvalidOperationException("Respuesta vacía del gateway");
                }

                _logger.LogInformation(
                    "Pago procesado. Reference: {Reference}, ResponseCode: {ResponseCode}, AuthCode: {AuthCode}",
                    request.Reference,
                    result.ResponseCode,
                    result.AuthCode ?? "N/A");

                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex,
                    "Error de conexión con el gateway. Reference: {Reference}",
                    request.Reference);
                throw new Exception("Error al conectar con el gateway de pagos", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error inesperado procesando pago. Reference: {Reference}",
                    request.Reference);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el historial de transacciones del gateway.
        /// </summary>
        public async Task<TransactionHistory[]> GetTransactionHistoryAsync(
            DateTime start,
            DateTime end,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Obteniendo historial de transacciones. Start: {Start}, End: {End}",
                    start, end);

                var response = await _httpClient.GetAsync(
                    $"{_baseUrl}Payment?start={start:yyyy-MM-ddTHH:mm:ss}&end={end:yyyy-MM-ddTHH:mm:ss}",
                    cancellationToken);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<TransactionHistory[]>(
                    cancellationToken: cancellationToken);

                _logger.LogInformation(
                    "Historial obtenido exitosamente. Transacciones: {Count}",
                    result?.Length ?? 0);

                return result ?? Array.Empty<TransactionHistory>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener historial de transacciones");
                throw new Exception("Error al conectar con el gateway para obtener historial", ex);
            }
        }

        /// <summary>
        /// Genera el hash HMAC SHA-512 según especificaciones del gateway de Falcon.
        /// Formato: ApiKey:Reference:CC:Name:Cvv:ExpMonth:ExpYear:Amount:Tax:Secret
        /// </summary>
        private string GenerateHash(PaymentGatewayRequest request)
        {
            // Formatear los valores según las especificaciones del gateway
            string expMonth = request.ExpMonth.ToString("D2");  // Formato XX (01, 02, ..., 12)
            string expYear = request.ExpYear.ToString("D2");    // Solo 2 dígitos (25, 26, etc.)
            string amount = request.Amount.ToString("F2");      // 2 decimales (100.00)
            string tax = request.Tax.ToString("F2");            // 2 decimales (18.00)

            // Concatenar según especificación del gateway
            // Orden: ApiKey:Reference:CC:Name:Cvv:ExpMonth:ExpYear:Amount:Tax:Secret
            string concat = $"{request.ApiKey}:{request.Reference}:{request.Cc}:{request.Name}:{request.Cvv}:{expMonth}:{expYear}:{amount}:{tax}:{_secret}";

            _logger.LogDebug("String para hash generada (longitud: {Length})", concat.Length);

            // Generar hash usando HMAC SHA-512
            using var hmacSha512 = new HMACSHA512(Encoding.UTF8.GetBytes(_secret));

            // Convertir string a bytes usando UTF-8
            byte[] data = Encoding.UTF8.GetBytes(concat);

            // Calcular hash
            byte[] hashBytes = hmacSha512.ComputeHash(data);

            // Convertir a string hexadecimal en minúsculas
            var hashString = new StringBuilder(hashBytes.Length * 2);
            foreach (var b in hashBytes)
            {
                hashString.Append(b.ToString("x2"));
            }

            string hash = hashString.ToString();

            _logger.LogDebug("Hash generado exitosamente (longitud: {Length})", hash.Length);

            return hash;
        }
    }
}
