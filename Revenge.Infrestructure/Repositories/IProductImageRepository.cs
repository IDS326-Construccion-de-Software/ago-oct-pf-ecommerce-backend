using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Revenge.Infrestructure.Entities;

namespace Revenge.Infrestructure.Repositories
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<Productimage>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Productimage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Productimage?> GetPrimaryByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Productimage> AddAsync(Productimage image, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
