using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.Data.Repositories
{
    /// <summary>
    /// Repositorio para la entidad Payment.
    /// Implementa la interfaz IPaymentRepository
    /// y maneja operaciones CRUD sobre la tabla Payments.
    /// </summary>
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RevengeDbContext _context;

        public PaymentRepository(RevengeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los pagos, incluyendo sus relaciones.
        /// </summary>
        public async Task<Payment[]?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .Include(p => p.User)
                .Include(p => p.Order)
                .Include(p => p.Invoice)
                .Include(p => p.PaymentMethod)
                .OrderByDescending(p => p.CreatedAt)
                .ToArrayAsync(cancellationToken);
        }

        /// <summary>
        /// Busca un pago por su ID, con todas sus relaciones.
        /// </summary>
        public async Task<Payment?> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .Include(p => p.User)
                .Include(p => p.Order)
                .Include(p => p.Invoice)
                .Include(p => p.PaymentMethod)
                .FirstOrDefaultAsync(p => p.Id == paymentId, cancellationToken);
        }

        /// <summary>
        /// Obtiene todos los pagos de un usuario específico.
        /// </summary>
        public async Task<Payment[]?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .Include(p => p.Invoice)
                .Include(p => p.PaymentMethod)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToArrayAsync(cancellationToken);
        }

        /// <summary>
        /// Obtiene todos los pagos asociados a una orden.
        /// </summary>
        public async Task<Payment[]?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .Include(p => p.User)
                .Include(p => p.PaymentMethod)
                .Include(p => p.Invoice)
                .Where(p => p.OrderId == orderId)
                .OrderByDescending(p => p.CreatedAt)
                .ToArrayAsync(cancellationToken);
        }

        /// <summary>
        /// Busca un pago por su referencia de transacción.
        /// </summary>
        public async Task<Payment?> GetByTransactionReferenceAsync(string transactionRef, CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .Include(p => p.User)
                .Include(p => p.Order)
                .Include(p => p.Invoice)
                .Include(p => p.PaymentMethod)
                .FirstOrDefaultAsync(p => p.TransactionReference == transactionRef, cancellationToken);
        }

        /// <summary>
        /// Agrega un nuevo pago a la base de datos.
        /// </summary>
        public async Task<bool> AddAsync(Payment newPayment, CancellationToken cancellationToken = default)
        {
            await _context.Payments.AddAsync(newPayment, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// <summary>
        /// Actualiza un pago existente.
        /// Si no existe, devuelve false.
        /// </summary>
        public async Task<bool> UpdateAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Payments.AnyAsync(p => p.Id == payment.Id, cancellationToken);
            if (!exists) return false;

            payment.UpdatedAt = DateTime.UtcNow;
            _context.Payments.Update(payment);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// <summary>
        /// Elimina un pago por su ID.
        /// </summary>
        public async Task<bool> DeleteAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var payment = await _context.Payments.FindAsync(new object[] { paymentId }, cancellationToken);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// <summary>
        /// Verifica si existe un pago con el ID indicado.
        /// </summary>
        public async Task<bool> ExistsAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            return await _context.Payments.AnyAsync(p => p.Id == paymentId, cancellationToken);
        }
    }
}
