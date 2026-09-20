using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class PricingPlanRepository : IPricingPlanRepository
{
    private readonly ApplicationDbContext _db;

    public PricingPlanRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PricingPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PricingPlans
            .Include(x => x.ProductOffering)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PricingPlan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PricingPlans
            .AsNoTracking()
            .Include(x => x.ProductOffering)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PricingPlan pricingPlan, CancellationToken cancellationToken)
    {
        _db.PricingPlans.Add(pricingPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PricingPlan pricingPlan, CancellationToken cancellationToken)
    {
        _db.PricingPlans.Update(pricingPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PricingPlan pricingPlan, CancellationToken cancellationToken)
    {
        _db.PricingPlans.Remove(pricingPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
