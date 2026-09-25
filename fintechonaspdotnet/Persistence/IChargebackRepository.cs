using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IChargebackRepository
{
    Task<Chargeback?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Chargeback>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Chargeback chargeback, CancellationToken cancellationToken);
    Task UpdateAsync(Chargeback chargeback, CancellationToken cancellationToken);
    Task DeleteAsync(Chargeback chargeback, CancellationToken cancellationToken);


}
