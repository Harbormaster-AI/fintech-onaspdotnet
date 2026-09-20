using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface ISecurityRepository
{
    Task<Security?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Security>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Security security, CancellationToken cancellationToken);
    Task UpdateAsync(Security security, CancellationToken cancellationToken);
    Task DeleteAsync(Security security, CancellationToken cancellationToken);
}
