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
}
