using Revenge.Core.Models;
using Revenge.Infrestructure.Entities;

namespace Revenge.Infrestructure.Repositories
{
    public interface IShoppingcartRepository
    {
        Task<ShoppingCartDTO[]> FindCartsByUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<ShoppingCartDTO?> FindCartByIdAsync(
            Guid cartId,
            CancellationToken cancellationToken = default);

        Task<bool> AddCartAsync(
            Shoppingcart newCart,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateCartAsync(
            Shoppingcart updatedCart,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteCartAsync(
            Guid cartId,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            Guid cartId,
            CancellationToken cancellationToken = default);
    }
}
