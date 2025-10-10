using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.Data.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly RevengeDbContext _context;

        public CartItemRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<Cartitem[]?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Cartitems
                .Include(c => c.Product)
                .Include(c => c.Cart)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Cartitem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Cartitems
                .Include(c => c.Product)
                .Include(c => c.Cart)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Cartitem[]?> GetByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            return await _context.Cartitems
                .Include(c => c.Product)
                .Where(c => c.CartId == cartId)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<bool> AddAsync(Cartitem item, CancellationToken cancellationToken = default)
        {
            await _context.Cartitems.AddAsync(item, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(Cartitem item, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Cartitems.AnyAsync(c => c.Id == item.Id, cancellationToken);
            if (!exists) return false;

            item.UpdatedAt = DateTime.UtcNow;
            _context.Cartitems.Update(item);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var item = await _context.Cartitems.FindAsync(new object[] { id }, cancellationToken);
            if (item == null) return false;

            _context.Cartitems.Remove(item);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Cartitems.AnyAsync(c => c.Id == id, cancellationToken);
        }
    }
}
