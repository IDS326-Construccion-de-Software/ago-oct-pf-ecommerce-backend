using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Revenge.Infrestructure.Entities;

namespace Revenge.Infrestructure.Repositories
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<Productimage>> GetAllAsync(CancellationToken cancellationToken);
        Task<Productimage?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Productimage?> GetPrimaryByProductIdAsync(Guid productId, CancellationToken cancellationToken);
        Task<Productimage> AddAsync(Productimage image, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
