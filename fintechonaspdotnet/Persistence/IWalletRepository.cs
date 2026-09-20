using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IWalletRepository
{
    Task<Wallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Wallet>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Wallet wallet, CancellationToken cancellationToken);
    Task UpdateAsync(Wallet wallet, CancellationToken cancellationToken);
    Task DeleteAsync(Wallet wallet, CancellationToken cancellationToken);
}
