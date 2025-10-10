using Revenge.Infrestructure.Entities;

namespace Revenge.Infrestructure.Repositories
{
    public interface IShoppingcartRepository
    {
        Task<Shoppingcart[]?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Shoppingcart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken = default);
        Task<Shoppingcart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(Shoppingcart cart, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Shoppingcart cart, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid cartId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid cartId, CancellationToken cancellationToken = default);
    }
}
