using Revenge.Infrestructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenge.Infrestructure.Repositories
{
    /// Operaciones sobre la entidad Category.
    public interface ICategoryRepository
    {
        Task<Category[]?> GetAllAsync(
            CancellationToken cancellationToken = default
            );
        Task<Category?> GetByNameAsync(
            string name, CancellationToken cancellationToken = default
            );
        Task<bool> AddAsync(
            Category newCategory, CancellationToken cancellationToken = default
            );
        Task<bool> UpdateAsync(
            Category category, CancellationToken cancellationToken = default
            );
        Task<bool> DeleteAsync(
            Guid categoryId, CancellationToken cancellationToken = default
            );
        Task<bool> ExistsAsync(
            Guid categoryId, CancellationToken cancellationToken = default
            );
    }
}
