
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly RevengeDbContext _context;

        public InvoiceRepository(RevengeDbContext context)
        {
            _context = context;
        }

        public Task<Invoice[]?> FindInvoicesByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();

        }
        public Task<Invoice?> FindInvoiceByIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();

        }
        public Task<bool> AddInvoiceAsync(Invoice newInvoice, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public virtual Task<bool> SoftDeleteInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public virtual Task<bool> DeleteInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}