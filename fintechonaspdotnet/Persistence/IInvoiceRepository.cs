using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Invoice invoice, CancellationToken cancellationToken);
    Task UpdateAsync(Invoice invoice, CancellationToken cancellationToken);
    Task DeleteAsync(Invoice invoice, CancellationToken cancellationToken);
}
