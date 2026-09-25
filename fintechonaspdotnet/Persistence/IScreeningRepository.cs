using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IScreeningRepository
{
    Task<Screening?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Screening>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Screening screening, CancellationToken cancellationToken);
    Task UpdateAsync(Screening screening, CancellationToken cancellationToken);
    Task DeleteAsync(Screening screening, CancellationToken cancellationToken);

    Task AddToAlertsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAlertsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
