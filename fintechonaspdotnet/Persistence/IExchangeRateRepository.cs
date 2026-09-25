using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IExchangeRateRepository
{
    Task<ExchangeRate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ExchangeRate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken);
    Task UpdateAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken);
    Task DeleteAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken);

    Task AddToUsedByQuotesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromUsedByQuotesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
