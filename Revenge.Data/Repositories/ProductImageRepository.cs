using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;

namespace Revenge.Infrestructure.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly RevengeDbContext _context;

        public ProductImageRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Productimage>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Productimages
                .Include(p => p.Product)
                .ToListAsync(cancellationToken);
        }

        public async Task<Productimage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Productimages.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Productimage?> GetPrimaryByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Productimages
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.IsPrimary, cancellationToken);
        }

        public async Task<Productimage> AddAsync(Productimage image, CancellationToken cancellationToken = default)
        {
            // Verifica que el producto exista
            var productExists = await _context.Products.AnyAsync(p => p.Id == image.ProductId, cancellationToken);
            if (!productExists)
                throw new ArgumentException("El producto especificado no existe.");

            // Si la imagen es principal, desmarcar otras
            if (image.IsPrimary)
            {
                var existingPrimary = await _context.Productimages
                    .Where(pi => pi.ProductId == image.ProductId && pi.IsPrimary)
                    .ToListAsync(cancellationToken);

                foreach (var img in existingPrimary)
                {
                    img.IsPrimary = false;
                }
            }

            _context.Productimages.Add(image);
            await _context.SaveChangesAsync(cancellationToken);
            return image;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var image = await _context.Productimages.FindAsync(new object[] { id }, cancellationToken);
            if (image == null)
                return false;

            _context.Productimages.Remove(image);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
