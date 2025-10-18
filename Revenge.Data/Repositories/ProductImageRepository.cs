using Microsoft.EntityFrameworkCore;
using Revenge.Core.Models;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Revenge.Infrestructure.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly RevengeDbContext _context;

        public ProductImageRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImageDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _context.Productimages
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return entities.Select(e => new ProductImageDTO
            {
                Id = e.Id,
                ProductId = e.ProductId,
                Url = e.Url,
                IsPrimary = e.IsPrimary,
                Order = e.Order
            });
        }

        public async Task<ProductImageDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Productimages
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            return entity == null ? null : new ProductImageDTO
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                Url = entity.Url,
                IsPrimary = entity.IsPrimary,
                Order = entity.Order
            };
        }

        public async Task<ProductImageDTO?> GetPrimaryByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Productimages
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ProductId == productId && e.IsPrimary, cancellationToken);

            return entity == null ? null : new ProductImageDTO
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                Url = entity.Url,
                IsPrimary = entity.IsPrimary,
                Order = entity.Order
            };
        }

        public async Task<ProductImageDTO> AddAsync(ProductImageDTO dto, CancellationToken cancellationToken = default)
        {
            var productExists = await _context.Products.AnyAsync(p => p.Id == dto.ProductId, cancellationToken);
            if (!productExists)
                throw new ArgumentException("El producto especificado no existe.");

            if (dto.IsPrimary)
            {
                var existingPrimary = await _context.Productimages
                    .Where(pi => pi.ProductId == dto.ProductId && pi.IsPrimary)
                    .ToListAsync(cancellationToken);

                foreach (var img in existingPrimary)
                    img.IsPrimary = false;
            }

            var entity = new Productimage
            {
                Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                ProductId = dto.ProductId,
                Url = dto.Url,
                IsPrimary = dto.IsPrimary,
                Order = dto.Order
            };

            _context.Productimages.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Productimages.FindAsync(new object[] { id }, cancellationToken);
            if (entity == null)
                return false;

            _context.Productimages.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
