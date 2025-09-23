using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Invoice[]?> FindInvoicesByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            Invoice[]? userInvoices = await _context.Invoices.Where(i => i.UserId == userId).ToArrayAsync(cancellationToken);
            return userInvoices;
        }

        public async Task<Invoice?> FindInvoiceByIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            Invoice? invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == invoiceId, cancellationToken);
            return invoice;
        }

        public async Task<bool> AddInvoiceAsync(Invoice newInvoice, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(newInvoice, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
