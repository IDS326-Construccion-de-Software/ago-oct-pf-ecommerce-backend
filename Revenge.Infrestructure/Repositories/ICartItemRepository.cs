using Revenge.Infrestructure.Entities;

namespace Revenge.Infrestructure.Repositories
{
    public interface ICartItemRepository
    {
        Task<Cartitem[]?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Cartitem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Cartitem[]?> GetByCartIdAsync(Guid cartId, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(Cartitem item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Cartitem item, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
