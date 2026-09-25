using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IFXDealRepository
{
    Task<FXDeal?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FXDeal>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(FXDeal fXDeal, CancellationToken cancellationToken);
    Task UpdateAsync(FXDeal fXDeal, CancellationToken cancellationToken);
    Task DeleteAsync(FXDeal fXDeal, CancellationToken cancellationToken);

    Task AddToPaymentOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
