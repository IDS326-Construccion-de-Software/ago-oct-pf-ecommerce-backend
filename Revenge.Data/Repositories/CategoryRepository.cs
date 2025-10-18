using Microsoft.EntityFrameworkCore;
using Revenge.Core.Models;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Revenge.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RevengeDbContext _context;

        public CategoryRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
        }

        public async Task<CategoryDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            return category == null ? null : new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDTO?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower(), cancellationToken);

            return category == null ? null : new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<bool> AddAsync(CategoryDTO dto, CancellationToken cancellationToken = default)
        {
            var entity = new Category
            {
                Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description
            };

            await _context.Categories.AddAsync(entity, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(CategoryDTO dto, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Categories.AnyAsync(c => c.Id == dto.Id, cancellationToken);
            if (!exists) return false;

            var entity = await _context.Categories.FirstAsync(c => c.Id == dto.Id, cancellationToken);
            entity.Name = dto.Name;
            entity.Description = dto.Description;

            _context.Categories.Update(entity);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories.FindAsync(new object[] { id }, cancellationToken);
            if (category == null) return false;

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id, cancellationToken);
        }
    }
}
