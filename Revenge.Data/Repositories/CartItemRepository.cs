using Microsoft.EntityFrameworkCore;
using Revenge.Core.Models;
using Revenge.Data.Context;
using Revenge.Infrestructure.Repositories;
using Revenge.Infrestructure.Entities;

namespace Revenge.Data.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly RevengeDbContext _context;

        public CartItemRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItemDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var items = await _context.Cartitems.AsNoTracking().ToListAsync(cancellationToken);
            return items.Select(c => new CartItemDTO
            {
                Id = c.Id,
                CartId = c.CartId,
                ProductId = c.ProductId,
                Quantity = c.Quantity
            });
        }

        public async Task<CartItemDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var c = await _context.Cartitems.FindAsync(new object[] { id }, cancellationToken);
            return c == null ? null : new CartItemDTO
            {
                Id = c.Id,
                CartId = c.CartId,
                ProductId = c.ProductId,
                Quantity = c.Quantity
            };
        }

        public async Task<IEnumerable<CartItemDTO>> GetByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            var items = await _context.Cartitems
                .Where(c => c.CartId == cartId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return items.Select(c => new CartItemDTO
            {
                Id = c.Id,
                CartId = c.CartId,
                ProductId = c.ProductId,
                Quantity = c.Quantity
            });
        }

        public async Task<bool> AddAsync(CartItemDTO dto, CancellationToken cancellationToken = default)
        {
            var entity = new Cartitem
            {
                Id = dto.Id,
                CartId = dto.CartId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };

            await _context.Cartitems.AddAsync(entity, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(CartItemDTO dto, CancellationToken cancellationToken = default)
        {
            var existing = await _context.Cartitems.FirstOrDefaultAsync(c => c.Id == dto.Id, cancellationToken);
            if (existing == null) return false;

            existing.Quantity = dto.Quantity;
            existing.CartId = dto.CartId;
            existing.ProductId = dto.ProductId;

            _context.Cartitems.Update(existing);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Cartitems.FindAsync(new object[] { id }, cancellationToken);
            if (entity == null) return false;

            _context.Cartitems.Remove(entity);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Cartitems.AnyAsync(c => c.Id == id, cancellationToken);
        }
    }
}
