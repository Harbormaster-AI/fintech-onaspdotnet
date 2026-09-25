using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ILoanTransactionRepository
{
    Task<LoanTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LoanTransaction>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(LoanTransaction loanTransaction, CancellationToken cancellationToken);
    Task UpdateAsync(LoanTransaction loanTransaction, CancellationToken cancellationToken);
    Task DeleteAsync(LoanTransaction loanTransaction, CancellationToken cancellationToken);


}
