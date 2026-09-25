using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ISecurityRepository
{
    Task<Security?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Security>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Security security, CancellationToken cancellationToken);
    Task UpdateAsync(Security security, CancellationToken cancellationToken);
    Task DeleteAsync(Security security, CancellationToken cancellationToken);

    Task AddToPositionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPositionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTradesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTradesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
