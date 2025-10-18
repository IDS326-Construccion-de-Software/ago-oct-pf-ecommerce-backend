using Revenge.Core.Models;
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
        Task<ProductDTO[]?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ProductDTO?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(ProductDTO newProduct, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(ProductDTO product, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid productId, CancellationToken cancellationToken = default);

    }
}
