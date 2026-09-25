
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class FeeScheduleRepository : IFeeScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public FeeScheduleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<FeeSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.FeeSchedules
            .Include(x => x.PricingPlan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FeeSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.FeeSchedules
            .AsNoTracking()
            .Include(x => x.PricingPlan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FeeSchedule feeSchedule, CancellationToken cancellationToken)
    {
        _db.FeeSchedules.Add(feeSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FeeSchedule feeSchedule, CancellationToken cancellationToken)
    {
        _db.FeeSchedules.Update(feeSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FeeSchedule feeSchedule, CancellationToken cancellationToken)
    {
        _db.FeeSchedules.Remove(feeSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
