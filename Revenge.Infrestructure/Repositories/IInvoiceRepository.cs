
using Revenge.Core.Models;
using Revenge.Infrestructure.Entities;


namespace Revenge.Infrestructure.Repositories
{
  public interface IInvoiceRepository
  {
    Task<InvoiceDTO[]> FindInvoicesByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<InvoiceDTO?> FindInvoiceByIdAsync(
      Guid invoiceId,
      CancellationToken cancellationToken = default);

    Task<bool> AddInvoiceAsync(
      Invoice newInvoice,
      CancellationToken cancellationToken = default);

    //Delete is only to development
    Task<bool> DeleteInvoiceAsync(
      Guid invoiceId,
      CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
    Guid invoiceId,
    CancellationToken cancellationToken = default);
  }

}