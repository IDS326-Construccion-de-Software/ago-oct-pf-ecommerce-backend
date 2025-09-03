using Revenge.Infrestructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenge.Infrestructure.Repositories
{
    public interface IProductRepository
    {
        Task<Product[]?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(Product newProduct, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid productId, CancellationToken cancellationToken = default);

    }
}
