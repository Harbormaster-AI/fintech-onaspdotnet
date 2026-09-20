using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Account>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Account account, CancellationToken cancellationToken);
    Task UpdateAsync(Account account, CancellationToken cancellationToken);
    Task DeleteAsync(Account account, CancellationToken cancellationToken);
}
