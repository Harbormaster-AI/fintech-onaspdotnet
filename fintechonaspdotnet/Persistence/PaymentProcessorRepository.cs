using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class PaymentProcessorRepository : IPaymentProcessorRepository
{
    private readonly ApplicationDbContext _db;

    public PaymentProcessorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PaymentProcessor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PaymentProcessors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PaymentProcessor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PaymentProcessors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken)
    {
        _db.PaymentProcessors.Add(paymentProcessor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken)
    {
        _db.PaymentProcessors.Update(paymentProcessor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken)
    {
        _db.PaymentProcessors.Remove(paymentProcessor);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
