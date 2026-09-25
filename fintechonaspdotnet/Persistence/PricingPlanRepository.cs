
using fintechonaspdotnet.Contracts;
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


    public async Task AddToFeeSchedulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FeeSchedules
            .Where(feeSchedule =>
                request.ChildIds.Contains(feeSchedule.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    feeSchedule =>
                        EF.Property<Guid?>(
                            feeSchedule,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromFeeSchedulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FeeSchedules
            .Where(feeSchedule =>
                request.ChildIds.Contains(feeSchedule.Id) &&
                EF.Property<Guid?>(
                    feeSchedule,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    feeSchedule =>
                        EF.Property<Guid?>(
                            feeSchedule,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToLimitsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.UsageLimits
            .Where(usageLimit =>
                request.ChildIds.Contains(usageLimit.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    usageLimit =>
                        EF.Property<Guid?>(
                            usageLimit,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLimitsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.UsageLimits
            .Where(usageLimit =>
                request.ChildIds.Contains(usageLimit.Id) &&
                EF.Property<Guid?>(
                    usageLimit,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    usageLimit =>
                        EF.Property<Guid?>(
                            usageLimit,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
