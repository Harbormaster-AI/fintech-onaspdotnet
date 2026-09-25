using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ICardTokenizationRepository
{
    Task<CardTokenization?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CardTokenization>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CardTokenization cardTokenization, CancellationToken cancellationToken);
    Task UpdateAsync(CardTokenization cardTokenization, CancellationToken cancellationToken);
    Task DeleteAsync(CardTokenization cardTokenization, CancellationToken cancellationToken);


}
