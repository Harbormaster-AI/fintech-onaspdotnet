using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IAccountStatementRepository
{
    Task<AccountStatement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AccountStatement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AccountStatement accountStatement, CancellationToken cancellationToken);
    Task UpdateAsync(AccountStatement accountStatement, CancellationToken cancellationToken);
    Task DeleteAsync(AccountStatement accountStatement, CancellationToken cancellationToken);


}
