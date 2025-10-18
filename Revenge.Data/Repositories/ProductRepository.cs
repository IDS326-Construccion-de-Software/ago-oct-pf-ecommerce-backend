using Microsoft.EntityFrameworkCore;
using Revenge.Core.Models;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly RevengeDbContext _context;

        public ProductRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDTO[]> FindAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    Brand = p.Brand
                })
                .ToArrayAsync(cancellationToken);
        }

        public async Task<ProductDTO?> FindByIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == productId)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    Brand = p.Brand
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> AddAsync(Product newProduct, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(newProduct, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(Product updatedProduct, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Products.AnyAsync(p => p.Id == updatedProduct.Id, cancellationToken);
            if (!exists) return false;

            updatedProduct.UpdatedAt = DateTime.UtcNow;
            _context.Products.Update(updatedProduct);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var product = new Product { Id = productId };
            _context.Attach(product);
            _context.Remove(product);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ExistsAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.AnyAsync(p => p.Id == productId, cancellationToken);
        }
    }
}
