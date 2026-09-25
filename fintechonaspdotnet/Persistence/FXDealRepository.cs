
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class FXDealRepository : IFXDealRepository
{
    private readonly ApplicationDbContext _db;

    public FXDealRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<FXDeal?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.FXDeals
            .Include(x => x.Quote)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FXDeal>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.FXDeals
            .AsNoTracking()
            .Include(x => x.Quote)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FXDeal fXDeal, CancellationToken cancellationToken)
    {
        _db.FXDeals.Add(fXDeal);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FXDeal fXDeal, CancellationToken cancellationToken)
    {
        _db.FXDeals.Update(fXDeal);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FXDeal fXDeal, CancellationToken cancellationToken)
    {
        _db.FXDeals.Remove(fXDeal);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPaymentOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentOrders
            .Where(paymentOrder =>
                request.ChildIds.Contains(paymentOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentOrder =>
                        EF.Property<Guid?>(
                            paymentOrder,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPaymentOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentOrders
            .Where(paymentOrder =>
                request.ChildIds.Contains(paymentOrder.Id) &&
                EF.Property<Guid?>(
                    paymentOrder,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentOrder =>
                        EF.Property<Guid?>(
                            paymentOrder,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
