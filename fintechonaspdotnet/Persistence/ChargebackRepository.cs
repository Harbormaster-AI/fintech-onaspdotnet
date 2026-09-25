
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class ChargebackRepository : IChargebackRepository
{
    private readonly ApplicationDbContext _db;

    public ChargebackRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Chargeback?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Chargebacks
            .Include(x => x.Dispute)
            .Include(x => x.Transaction)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Chargeback>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Chargebacks
            .AsNoTracking()
            .Include(x => x.Dispute)
            .Include(x => x.Transaction)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Chargeback chargeback, CancellationToken cancellationToken)
    {
        _db.Chargebacks.Add(chargeback);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Chargeback chargeback, CancellationToken cancellationToken)
    {
        _db.Chargebacks.Update(chargeback);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Chargeback chargeback, CancellationToken cancellationToken)
    {
        _db.Chargebacks.Remove(chargeback);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
