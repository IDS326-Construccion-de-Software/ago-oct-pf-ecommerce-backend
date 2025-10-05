using Revenge.Infrestructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Revenge.Infrestructure.Repositories
{
    public interface IOrderRepository
    {
        Task<Order[]?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(Order newOrder, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Order order, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}
