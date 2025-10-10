using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.Data.Repositories
{
    public class ShoppingcartRepository : IShoppingcartRepository
    {
        private readonly RevengeDbContext _context;

        public ShoppingcartRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<Shoppingcart[]?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Shoppingcarts
                .Include(c => c.Cartitems)
                .Include(c => c.User)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Shoppingcart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            return await _context.Shoppingcarts
                .Include(c => c.Cartitems)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);
        }

        public async Task<Shoppingcart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Shoppingcarts
                .Include(c => c.Cartitems)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        }

        public async Task<bool> AddAsync(Shoppingcart cart, CancellationToken cancellationToken = default)
        {
            await _context.Shoppingcarts.AddAsync(cart, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(Shoppingcart cart, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Shoppingcarts.AnyAsync(c => c.Id == cart.Id, cancellationToken);
            if (!exists) return false;

            cart.UpdatedAt = DateTime.UtcNow;
            _context.Shoppingcarts.Update(cart);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            var cart = await _context.Shoppingcarts.FindAsync(new object[] { cartId }, cancellationToken);
            if (cart == null) return false;

            _context.Shoppingcarts.Remove(cart);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ExistsAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            return await _context.Shoppingcarts.AnyAsync(c => c.Id == cartId, cancellationToken);
        }
    }
}
