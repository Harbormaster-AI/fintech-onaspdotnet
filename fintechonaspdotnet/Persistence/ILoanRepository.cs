using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Loan loan, CancellationToken cancellationToken);
    Task UpdateAsync(Loan loan, CancellationToken cancellationToken);
    Task DeleteAsync(Loan loan, CancellationToken cancellationToken);
}
