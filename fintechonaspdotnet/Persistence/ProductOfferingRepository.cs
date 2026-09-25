
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class ProductOfferingRepository : IProductOfferingRepository
{
    private readonly ApplicationDbContext _db;

    public ProductOfferingRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProductOffering?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ProductOfferings
            .Include(x => x.Institution)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductOffering>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ProductOfferings
            .AsNoTracking()
            .Include(x => x.Institution)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProductOffering productOffering, CancellationToken cancellationToken)
    {
        _db.ProductOfferings.Add(productOffering);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductOffering productOffering, CancellationToken cancellationToken)
    {
        _db.ProductOfferings.Update(productOffering);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductOffering productOffering, CancellationToken cancellationToken)
    {
        _db.ProductOfferings.Remove(productOffering);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPricingPlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PricingPlans
            .Where(pricingPlan =>
                request.ChildIds.Contains(pricingPlan.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    pricingPlan =>
                        EF.Property<Guid?>(
                            pricingPlan,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPricingPlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PricingPlans
            .Where(pricingPlan =>
                request.ChildIds.Contains(pricingPlan.Id) &&
                EF.Property<Guid?>(
                    pricingPlan,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    pricingPlan =>
                        EF.Property<Guid?>(
                            pricingPlan,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
