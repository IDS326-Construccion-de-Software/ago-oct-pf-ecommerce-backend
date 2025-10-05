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
    /// Implementa la interfaz ICategoryRepository
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RevengeDbContext _context;

        public CategoryRepository(RevengeDbContext context)
        {
            _context = context;
        }

        /// Obtiene todas las categorías.
        public async Task<Category[]?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Categories.ToArrayAsync(cancellationToken);
        }

        /// Busca una categoría por nombre (case-insensitive).
        public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        /// Agrega una nueva categoría.
        public async Task<bool> AddAsync(Category newCategory, CancellationToken cancellationToken = default)
        {
            await _context.Categories.AddAsync(newCategory, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// Actualiza una categoría existente.
        public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Categories.AnyAsync(c => c.Id == category.Id, cancellationToken);
            if (!exists) return false;

            category.UpdatedAt = DateTime.UtcNow;
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// Elimina una categoría por ID.
        public async Task<bool> DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories.FindAsync(new object[] { categoryId }, cancellationToken);
            if (category == null) return false;

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// Verifica si existe una categoría por ID.
        public async Task<bool> ExistsAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
        }
    }
}