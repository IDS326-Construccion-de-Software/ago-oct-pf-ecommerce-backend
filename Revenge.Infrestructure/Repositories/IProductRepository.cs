using Revenge.Core.Models;
using Revenge.Infrestructure.Entities;

namespace Revenge.Infrestructure.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDTO[]> FindAllAsync(
            CancellationToken cancellationToken = default);

        Task<ProductDTO?> FindByIdAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        Task<bool> AddAsync(
            Product newProduct,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(
            Product updatedProduct,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            Guid productId,
            CancellationToken cancellationToken = default);
    }
}
