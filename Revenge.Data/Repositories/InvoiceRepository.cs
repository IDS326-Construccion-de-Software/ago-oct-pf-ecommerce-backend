using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;
using Revenge.Core.Models;
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

        public async Task<InvoiceDTO[]> FindInvoicesByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Invoices
                .Where(i => i.UserId == userId)
                .AsNoTracking()
                .Select(i => new InvoiceDTO
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    UserId = i.UserId,
                    IssuedAt = i.IssuedAt,
                    Total = i.Total,
                    Tax = i.Tax,
                    Url = i.Url
                })
                .ToArrayAsync(cancellationToken);
        }

        public async Task<InvoiceDTO?> FindInvoiceByIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await _context.Invoices
                .AsNoTracking()
                .Where(i => i.Id == invoiceId)
                .Select(i => new InvoiceDTO
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    UserId = i.UserId,
                    IssuedAt = i.IssuedAt,
                    Total = i.Total,
                    Tax = i.Tax,
                    Url = i.Url
                })
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<bool> AddInvoiceAsync(Invoice newInvoice, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(newInvoice, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            Invoice invoice = new Invoice
            {
                Id = invoiceId
            };

            _context.Attach(invoice);
            _context.Remove(invoice);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ExistsAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await _context.Invoices.AnyAsync(i => i.Id == invoiceId, cancellationToken);
        }

    }
}
