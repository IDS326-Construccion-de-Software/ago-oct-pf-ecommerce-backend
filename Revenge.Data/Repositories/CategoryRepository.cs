using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Revenge.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RevengeDbContext _context;

        public CategoryRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public async Task<Category[]?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Categories.ToArrayAsync(cancellationToken);
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        public async Task<bool> AddAsync(Category newCategory, CancellationToken cancellationToken = default)
        {
            await _context.Categories.AddAsync(newCategory, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Categories.AnyAsync(c => c.Id == category.Id, cancellationToken);
            if (!exists) return false;

            category.UpdatedAt = DateTime.UtcNow;
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories.FindAsync(new object[] { categoryId }, cancellationToken);
            if (category == null) return false;

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ExistsAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
        }
    }
}