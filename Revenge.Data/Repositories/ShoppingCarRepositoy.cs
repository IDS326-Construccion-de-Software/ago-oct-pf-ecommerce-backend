using Microsoft.EntityFrameworkCore;
using Revenge.Core.Models;
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

        public async Task<ShoppingCartDTO[]> FindCartsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Shoppingcarts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => new ShoppingCartDTO
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToArrayAsync(cancellationToken);
        }

        public async Task<ShoppingCartDTO?> FindCartByIdAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            return await _context.Shoppingcarts
                .AsNoTracking()
                .Where(c => c.Id == cartId)
                .Select(c => new ShoppingCartDTO
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> AddCartAsync(Shoppingcart newCart, CancellationToken cancellationToken = default)
        {
            await _context.Shoppingcarts.AddAsync(newCart, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateCartAsync(Shoppingcart updatedCart, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Shoppingcarts.AnyAsync(c => c.Id == updatedCart.Id, cancellationToken);
            if (!exists) return false;

            updatedCart.UpdatedAt = DateTime.UtcNow;
            _context.Shoppingcarts.Update(updatedCart);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteCartAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            var cart = new Shoppingcart { Id = cartId };
            _context.Attach(cart);
            _context.Remove(cart);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ExistsAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            return await _context.Shoppingcarts.AnyAsync(c => c.Id == cartId, cancellationToken);
        }
    }
}
