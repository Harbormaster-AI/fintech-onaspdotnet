using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IDisputeRepository
{
    Task<Dispute?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Dispute>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Dispute dispute, CancellationToken cancellationToken);
    Task UpdateAsync(Dispute dispute, CancellationToken cancellationToken);
    Task DeleteAsync(Dispute dispute, CancellationToken cancellationToken);
}
