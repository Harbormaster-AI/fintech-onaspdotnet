
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class AppliedFeeRepository : IAppliedFeeRepository
{
    private readonly ApplicationDbContext _db;

    public AppliedFeeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AppliedFee?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AppliedFees
            .Include(x => x.PaymentOrder)
            .Include(x => x.Transaction)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AppliedFee>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AppliedFees
            .AsNoTracking()
            .Include(x => x.PaymentOrder)
            .Include(x => x.Transaction)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AppliedFee appliedFee, CancellationToken cancellationToken)
    {
        _db.AppliedFees.Add(appliedFee);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AppliedFee appliedFee, CancellationToken cancellationToken)
    {
        _db.AppliedFees.Update(appliedFee);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AppliedFee appliedFee, CancellationToken cancellationToken)
    {
        _db.AppliedFees.Remove(appliedFee);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
