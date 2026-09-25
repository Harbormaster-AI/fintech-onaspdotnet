using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Account>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Account account, CancellationToken cancellationToken);
    Task UpdateAsync(Account account, CancellationToken cancellationToken);
    Task DeleteAsync(Account account, CancellationToken cancellationToken);

    Task AddToTransactionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTransactionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCardsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCardsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToStatementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromStatementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToMandatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMandatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
