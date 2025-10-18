using Revenge.Core.Models;
using Revenge.Infrestructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenge.Infrestructure.Repositories
{
    public interface ICategoryRepository
    {
        Task<CategoryDTO[]?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CategoryDTO?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(CategoryDTO category, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(CategoryDTO category, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<CategoryDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
