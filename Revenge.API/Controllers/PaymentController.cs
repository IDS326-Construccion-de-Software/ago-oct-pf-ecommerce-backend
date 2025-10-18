using Microsoft.AspNetCore.Mvc;
using Revenge.Data.Models;
using Revenge.Infrestructure.Repositories;
using Revenge.Infrestructure.Services;
using Revenge.Infrestructure.Entities;

namespace Revenge.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar operaciones de pago.
    /// Procesa pagos a través del Payment Gateway e interactúa con el repositorio de pagos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentGatewayService _paymentGatewayService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(
            IPaymentRepository paymentRepository,
            IPaymentGatewayService paymentGatewayService,
            ILogger<PaymentController> logger)
        {
            _paymentRepository = paymentRepository;
            _paymentGatewayService = paymentGatewayService;
            _logger = logger;
        }

        /// <summary>
        /// Procesa un nuevo pago a través del Payment Gateway.
        /// </summary>
        /// <param name="paymentRequest">Datos del pago incluyendo información de la tarjeta</param>
        /// <returns>Resultado del procesamiento del pago</returns>
        /// <response code="200">Pago procesado exitosamente</response>
        /// <response code="400">Datos inválidos o pago rechazado</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDTO paymentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar que la tarjeta no esté vencida
            if (paymentRequest.IsCardExpired())
            {
                return BadRequest(new { error = "La tarjeta de crédito está vencida" });
            }

            _logger.LogInformation("Iniciando proceso de pago para la orden {OrderId}", paymentRequest.OrderId);

            // 1. Generar referencia única para la transacción
            string transactionReference = $"TXN-{Guid.NewGuid().ToString().Substring(0, 8)}-{DateTime.UtcNow.Ticks}";

            // 2. Crear y guardar la entidad de pago inicial con estado "pending"
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                UserId = paymentRequest.UserId,
                OrderId = paymentRequest.OrderId,
                InvoiceId = paymentRequest.InvoiceId,
                Amount = paymentRequest.Amount,
                PaymentMethodId = Guid.NewGuid(), // TODO: Obtener del método de pago real
                TransactionReference = transactionReference,
                PaymentStatus = "pending",
                CreatedAt = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment);
            _logger.LogInformation("Registro de pago inicial creado con ID {PaymentId}", payment.Id);

            try
            {
                // 3. Preparar la solicitud para el Payment Gateway
                var gatewayRequest = new PaymentGatewayRequest
                {
                    Reference = transactionReference,
                    Cc = paymentRequest.CreditCardNumber,
                    Name = paymentRequest.CardHolderName,
                    Cvv = paymentRequest.Cvv,
                    ExpMonth = paymentRequest.ExpirationMonth,
                    ExpYear = paymentRequest.ExpirationYear,
                    Amount = paymentRequest.Amount,
                    Tax = paymentRequest.Tax
                };

                _logger.LogInformation("Enviando solicitud de pago a la pasarela para el pago ID {PaymentId}", payment.Id);
                var gatewayResponse = await _paymentGatewayService.ProcessPaymentAsync(gatewayRequest);

                // 4. Actualizar la entidad de pago con la respuesta de la pasarela
                payment.AuthCode = gatewayResponse.AuthCode;
                payment.ResponseCode = gatewayResponse.ResponseCode;
                payment.GatewayErrorMessage = gatewayResponse.ErrorMsg;

                // Determinar si el pago fue exitoso (ResponseCode "00" o "0")
                bool isSuccessful = gatewayResponse.ResponseCode == "00" || gatewayResponse.ResponseCode == "0";
                payment.PaymentStatus = isSuccessful ? "completed" : "failed";
                payment.PaidAt = isSuccessful ? DateTime.UtcNow : null;

                await _paymentRepository.UpdateAsync(payment);
                _logger.LogInformation("Pago ID {PaymentId} actualizado con respuesta de la pasarela. Estado: {Status}", payment.Id, payment.PaymentStatus);

                // 5. Preparar y devolver la respuesta final al cliente
                var paymentResponse = new PaymentResponseDTO
                {
                    PaymentId = payment.Id,
                    TransactionReference = transactionReference,
                    AuthCode = payment.AuthCode,
                    ResponseCode = payment.ResponseCode,
                    ErrorMsg = payment.GatewayErrorMessage,
                    IsSuccessful = isSuccessful,
                    Amount = payment.Amount,
                    PaymentStatus = payment.PaymentStatus,
                    ProcessedAt = DateTime.UtcNow
                };

                if (isSuccessful)
                {
                    _logger.LogInformation("Pago procesado exitosamente. PaymentId: {PaymentId}", payment.Id);
                    return Ok(paymentResponse);
                }
                else
                {
                    _logger.LogWarning("Pago rechazado. PaymentId: {PaymentId}, ResponseCode: {ResponseCode}", payment.Id, payment.ResponseCode);
                    return BadRequest(paymentResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error catastrófico durante el procesamiento del pago ID {PaymentId}", payment.Id);

                // Actualizar el pago a "failed" en caso de error inesperado
                payment.PaymentStatus = "failed";
                payment.GatewayErrorMessage = "Error interno del servidor al procesar el pago.";
                await _paymentRepository.UpdateAsync(payment);

                return StatusCode(500, new { error = "Ocurrió un error inesperado al procesar el pago." });
            }
        }

        /// <summary>
        /// Obtiene un pago específico por su ID.
        /// </summary>
        /// <param name="id">ID del pago a buscar</param>
        /// <returns>Datos del pago</returns>
        /// <response code="200">Pago encontrado</response>
        /// <response code="404">Pago no encontrado</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            _logger.LogInformation("Buscando pago con ID {PaymentId}", id);
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                _logger.LogWarning("Pago con ID {PaymentId} no encontrado", id);
                return NotFound();
            }
            return Ok(payment);
        }

        /// <summary>
        /// Obtiene todos los pagos de un usuario específico.
        /// </summary>
        /// <param name="userId">ID del usuario</param>
        /// <returns>Lista de pagos del usuario</returns>
        /// <response code="200">Lista de pagos (puede estar vacía)</response>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPaymentsByUser(Guid userId)
        {
            _logger.LogInformation("Buscando pagos para el usuario con ID {UserId}", userId);
            var payments = await _paymentRepository.GetByUserIdAsync(userId);
            return Ok(payments);
        }
    }
}
