using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Loan loan, CancellationToken cancellationToken);
    Task UpdateAsync(Loan loan, CancellationToken cancellationToken);
    Task DeleteAsync(Loan loan, CancellationToken cancellationToken);

    Task AddToScheduleAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromScheduleAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCollateralAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCollateralAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTransactionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTransactionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
