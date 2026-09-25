
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class UsageLimitRepository : IUsageLimitRepository
{
    private readonly ApplicationDbContext _db;

    public UsageLimitRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<UsageLimit?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.UsageLimits
            .Include(x => x.PricingPlan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<UsageLimit>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.UsageLimits
            .AsNoTracking()
            .Include(x => x.PricingPlan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(UsageLimit usageLimit, CancellationToken cancellationToken)
    {
        _db.UsageLimits.Add(usageLimit);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UsageLimit usageLimit, CancellationToken cancellationToken)
    {
        _db.UsageLimits.Update(usageLimit);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(UsageLimit usageLimit, CancellationToken cancellationToken)
    {
        _db.UsageLimits.Remove(usageLimit);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
