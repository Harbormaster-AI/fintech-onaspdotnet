using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IAgreementRepository
{
    Task<Agreement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Agreement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Agreement agreement, CancellationToken cancellationToken);
    Task UpdateAsync(Agreement agreement, CancellationToken cancellationToken);
    Task DeleteAsync(Agreement agreement, CancellationToken cancellationToken);


}
