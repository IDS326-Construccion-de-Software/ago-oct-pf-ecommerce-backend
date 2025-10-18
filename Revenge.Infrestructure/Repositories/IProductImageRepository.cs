using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Revenge.Core.Models;

namespace Revenge.Infrestructure.Repositories
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImageDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ProductImageDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ProductImageDTO?> GetPrimaryByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<ProductImageDTO> AddAsync(ProductImageDTO image, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
