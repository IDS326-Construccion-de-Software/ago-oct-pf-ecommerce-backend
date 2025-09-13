using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;

namespace Revenge.Infrestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly RevengeDbContext _context;

        public ProductRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<Product[]?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products.ToArrayAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.id == productId, cancellationToken);
        }

        public async Task<bool> AddAsync(Product newProduct, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(newProduct, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            _context.Products.Update(product);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var product = await GetByIdAsync(productId, cancellationToken);
            if (product == null) return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ExistsAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.AnyAsync(p => p.id == productId, cancellationToken);
        }
    }
}
