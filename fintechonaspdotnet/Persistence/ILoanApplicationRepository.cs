using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ILoanApplicationRepository
{
    Task<LoanApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LoanApplication>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(LoanApplication loanApplication, CancellationToken cancellationToken);
    Task UpdateAsync(LoanApplication loanApplication, CancellationToken cancellationToken);
    Task DeleteAsync(LoanApplication loanApplication, CancellationToken cancellationToken);


}
