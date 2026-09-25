using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IConsentRepository
{
    Task<Consent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Consent>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Consent consent, CancellationToken cancellationToken);
    Task UpdateAsync(Consent consent, CancellationToken cancellationToken);
    Task DeleteAsync(Consent consent, CancellationToken cancellationToken);


}
