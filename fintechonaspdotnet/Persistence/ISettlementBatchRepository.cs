using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface ISettlementBatchRepository
{
    Task<SettlementBatch?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SettlementBatch>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SettlementBatch settlementBatch, CancellationToken cancellationToken);
    Task UpdateAsync(SettlementBatch settlementBatch, CancellationToken cancellationToken);
    Task DeleteAsync(SettlementBatch settlementBatch, CancellationToken cancellationToken);
}
