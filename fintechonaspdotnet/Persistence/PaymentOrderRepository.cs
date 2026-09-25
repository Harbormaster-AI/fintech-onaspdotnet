
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class PaymentOrderRepository : IPaymentOrderRepository
{
    private readonly ApplicationDbContext _db;

    public PaymentOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PaymentOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PaymentOrders
            .Include(x => x.SourceAccount)
            .Include(x => x.DestinationAccount)
            .Include(x => x.Beneficiary)
            .Include(x => x.FxDeal)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PaymentOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PaymentOrders
            .AsNoTracking()
            .Include(x => x.SourceAccount)
            .Include(x => x.DestinationAccount)
            .Include(x => x.Beneficiary)
            .Include(x => x.FxDeal)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PaymentOrder paymentOrder, CancellationToken cancellationToken)
    {
        _db.PaymentOrders.Add(paymentOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PaymentOrder paymentOrder, CancellationToken cancellationToken)
    {
        _db.PaymentOrders.Update(paymentOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentOrder paymentOrder, CancellationToken cancellationToken)
    {
        _db.PaymentOrders.Remove(paymentOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToTransactionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Transactions
            .Where(transaction =>
                request.ChildIds.Contains(transaction.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    transaction =>
                        EF.Property<Guid?>(
                            transaction,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTransactionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Transactions
            .Where(transaction =>
                request.ChildIds.Contains(transaction.Id) &&
                EF.Property<Guid?>(
                    transaction,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    transaction =>
                        EF.Property<Guid?>(
                            transaction,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToFeesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AppliedFees
            .Where(appliedFee =>
                request.ChildIds.Contains(appliedFee.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    appliedFee =>
                        EF.Property<Guid?>(
                            appliedFee,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromFeesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AppliedFees
            .Where(appliedFee =>
                request.ChildIds.Contains(appliedFee.Id) &&
                EF.Property<Guid?>(
                    appliedFee,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    appliedFee =>
                        EF.Property<Guid?>(
                            appliedFee,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
