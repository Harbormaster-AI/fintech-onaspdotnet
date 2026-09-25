using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IMerchantRepository
{
    Task<Merchant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Merchant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Merchant merchant, CancellationToken cancellationToken);
    Task UpdateAsync(Merchant merchant, CancellationToken cancellationToken);
    Task DeleteAsync(Merchant merchant, CancellationToken cancellationToken);

    Task AddToTerminalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTerminalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPaymentContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPayoutsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPayoutsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSettlementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSettlementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDisputesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDisputesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInvoicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInvoicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
