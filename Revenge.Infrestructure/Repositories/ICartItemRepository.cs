using Revenge.Core.Models;
using Revenge.Infrestructure.Entities;

namespace Revenge.Infrestructure.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItemDTO[]?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CartItemDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<CartItemDTO[]?> GetByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(CartItemDTO item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(CartItemDTO item, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
