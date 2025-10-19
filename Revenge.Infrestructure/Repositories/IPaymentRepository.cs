using Revenge.Infrestructure.Entities;

namespace Revenge.Infrestructure.Repositories
{
    /// <summary>
    /// Repositorio para operaciones CRUD de la entidad Payment
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Obtiene todos los pagos con sus relaciones
        /// </summary>
        Task<Payment[]?> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene un pago por su ID
        /// </summary>
        Task<Payment?> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene todos los pagos de un usuario específico
        /// </summary>
        Task<Payment[]?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene todos los pagos asociados a una orden
        /// </summary>
        Task<Payment[]?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Busca un pago por su referencia de transacción
        /// </summary>
        Task<Payment?> GetByTransactionReferenceAsync(string transactionRef, CancellationToken cancellationToken = default);

        /// <summary>
        /// Agrega un nuevo pago a la base de datos
        /// </summary>
        Task<bool> AddAsync(Payment newPayment, CancellationToken cancellationToken = default);

        /// <summary>
        /// Actualiza un pago existente
        /// </summary>
        Task<bool> UpdateAsync(Payment payment, CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina un pago por su ID
        /// </summary>
        Task<bool> DeleteAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifica si existe un pago con el ID indicado
        /// </summary>
        Task<bool> ExistsAsync(Guid paymentId, CancellationToken cancellationToken = default);
    }
}
