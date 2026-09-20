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
}
