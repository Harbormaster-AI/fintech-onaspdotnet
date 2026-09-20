using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IExchangeRateRepository
{
    Task<ExchangeRate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ExchangeRate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken);
    Task UpdateAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken);
    Task DeleteAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken);
}
