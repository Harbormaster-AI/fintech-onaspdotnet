using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IPricingPlanRepository
{
    Task<PricingPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PricingPlan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PricingPlan pricingPlan, CancellationToken cancellationToken);
    Task UpdateAsync(PricingPlan pricingPlan, CancellationToken cancellationToken);
    Task DeleteAsync(PricingPlan pricingPlan, CancellationToken cancellationToken);
}
