using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IDirectDebitMandateRepository
{
    Task<DirectDebitMandate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DirectDebitMandate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DirectDebitMandate directDebitMandate, CancellationToken cancellationToken);
    Task UpdateAsync(DirectDebitMandate directDebitMandate, CancellationToken cancellationToken);
    Task DeleteAsync(DirectDebitMandate directDebitMandate, CancellationToken cancellationToken);


}
