using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ISettlementBatchRepository
{
    Task<SettlementBatch?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SettlementBatch>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SettlementBatch settlementBatch, CancellationToken cancellationToken);
    Task UpdateAsync(SettlementBatch settlementBatch, CancellationToken cancellationToken);
    Task DeleteAsync(SettlementBatch settlementBatch, CancellationToken cancellationToken);

    Task AddToPayoutsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPayoutsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTransactionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTransactionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
