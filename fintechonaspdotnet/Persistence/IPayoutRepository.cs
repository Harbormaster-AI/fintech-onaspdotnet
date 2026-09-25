using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IPayoutRepository
{
    Task<Payout?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Payout>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Payout payout, CancellationToken cancellationToken);
    Task UpdateAsync(Payout payout, CancellationToken cancellationToken);
    Task DeleteAsync(Payout payout, CancellationToken cancellationToken);


}
