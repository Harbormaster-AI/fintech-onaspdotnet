using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Invoice invoice, CancellationToken cancellationToken);
    Task UpdateAsync(Invoice invoice, CancellationToken cancellationToken);
    Task DeleteAsync(Invoice invoice, CancellationToken cancellationToken);

    Task AddToPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
