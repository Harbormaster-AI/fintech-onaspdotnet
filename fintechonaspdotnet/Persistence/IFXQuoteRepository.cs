using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IFXQuoteRepository
{
    Task<FXQuote?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FXQuote>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(FXQuote fXQuote, CancellationToken cancellationToken);
    Task UpdateAsync(FXQuote fXQuote, CancellationToken cancellationToken);
    Task DeleteAsync(FXQuote fXQuote, CancellationToken cancellationToken);


}
