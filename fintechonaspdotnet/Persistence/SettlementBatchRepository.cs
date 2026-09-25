
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class SettlementBatchRepository : ISettlementBatchRepository
{
    private readonly ApplicationDbContext _db;

    public SettlementBatchRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SettlementBatch?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SettlementBatchs
            .Include(x => x.Processor)
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SettlementBatch>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SettlementBatchs
            .AsNoTracking()
            .Include(x => x.Processor)
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SettlementBatch settlementBatch, CancellationToken cancellationToken)
    {
        _db.SettlementBatchs.Add(settlementBatch);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SettlementBatch settlementBatch, CancellationToken cancellationToken)
    {
        _db.SettlementBatchs.Update(settlementBatch);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SettlementBatch settlementBatch, CancellationToken cancellationToken)
    {
        _db.SettlementBatchs.Remove(settlementBatch);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPayoutsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Payouts
            .Where(payout =>
                request.ChildIds.Contains(payout.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payout =>
                        EF.Property<Guid?>(
                            payout,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPayoutsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Payouts
            .Where(payout =>
                request.ChildIds.Contains(payout.Id) &&
                EF.Property<Guid?>(
                    payout,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payout =>
                        EF.Property<Guid?>(
                            payout,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToTransactionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Transactions
            .Where(transaction =>
                request.ChildIds.Contains(transaction.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    transaction =>
                        EF.Property<Guid?>(
                            transaction,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTransactionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Transactions
            .Where(transaction =>
                request.ChildIds.Contains(transaction.Id) &&
                EF.Property<Guid?>(
                    transaction,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    transaction =>
                        EF.Property<Guid?>(
                            transaction,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
