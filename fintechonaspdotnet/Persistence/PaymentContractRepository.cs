
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class PaymentContractRepository : IPaymentContractRepository
{
    private readonly ApplicationDbContext _db;

    public PaymentContractRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PaymentContract?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PaymentContracts
            .Include(x => x.Merchant)
            .Include(x => x.Acquirer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PaymentContract>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PaymentContracts
            .AsNoTracking()
            .Include(x => x.Merchant)
            .Include(x => x.Acquirer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PaymentContract paymentContract, CancellationToken cancellationToken)
    {
        _db.PaymentContracts.Add(paymentContract);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PaymentContract paymentContract, CancellationToken cancellationToken)
    {
        _db.PaymentContracts.Update(paymentContract);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentContract paymentContract, CancellationToken cancellationToken)
    {
        _db.PaymentContracts.Remove(paymentContract);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
