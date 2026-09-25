using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ICreditorRepository
{
    Task<Creditor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Creditor>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Creditor creditor, CancellationToken cancellationToken);
    Task UpdateAsync(Creditor creditor, CancellationToken cancellationToken);
    Task DeleteAsync(Creditor creditor, CancellationToken cancellationToken);

    Task AddToMandatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMandatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
