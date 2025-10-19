using Microsoft.EntityFrameworkCore;
using Revenge.Core.Models;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.Data.Repositories
{
    /// Repositorio para la entidad Order.
    /// Implementa la interfaz IOrderRepository
    /// y maneja operaciones CRUD sobre la tabla Orders.
    public class OrderRepository : IOrderRepository
    {
        private readonly RevengeDbContext _context;

        public OrderRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDTO[]?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    CartId = o.AddressId ?? Guid.Empty, 
                    OrderDate = o.PlacedAt,
                    Status = null, 
                    UpdatedAt = o.UpdatedAt
                })
                .ToArrayAsync(cancellationToken);
        }

        public async Task<OrderDTO?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == orderId)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    CartId = o.AddressId ?? Guid.Empty,
                    OrderDate = o.PlacedAt,
                    Status = null,
                    UpdatedAt = o.UpdatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// Agrega una nueva orden
        public async Task<bool> AddAsync(Order newOrder, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(newOrder, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// Actualiza una orden existente
        public async Task<bool> UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Orders.AnyAsync(o => o.Id == order.Id, cancellationToken);
            if (!exists) return false;

            order.UpdatedAt = DateTime.UtcNow;
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// Elimina una orden por su ID
        public async Task<bool> DeleteAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FindAsync(new object[] { orderId }, cancellationToken);
            if (order == null) return false;

            _context.Orders.Remove(order);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// Verifica si existe una orden
        public async Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.AnyAsync(o => o.Id == orderId, cancellationToken);
        }
    }
}
