using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IUsageLimitRepository
{
    Task<UsageLimit?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<UsageLimit>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(UsageLimit usageLimit, CancellationToken cancellationToken);
    Task UpdateAsync(UsageLimit usageLimit, CancellationToken cancellationToken);
    Task DeleteAsync(UsageLimit usageLimit, CancellationToken cancellationToken);


}
