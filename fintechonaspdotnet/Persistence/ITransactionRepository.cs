using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Transaction>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken);
    Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken);
    Task DeleteAsync(Transaction transaction, CancellationToken cancellationToken);
}
