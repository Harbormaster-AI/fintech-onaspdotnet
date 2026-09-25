
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class DirectDebitMandateRepository : IDirectDebitMandateRepository
{
    private readonly ApplicationDbContext _db;

    public DirectDebitMandateRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DirectDebitMandate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DirectDebitMandates
            .Include(x => x.Account)
            .Include(x => x.Creditor)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DirectDebitMandate>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DirectDebitMandates
            .AsNoTracking()
            .Include(x => x.Account)
            .Include(x => x.Creditor)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DirectDebitMandate directDebitMandate, CancellationToken cancellationToken)
    {
        _db.DirectDebitMandates.Add(directDebitMandate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DirectDebitMandate directDebitMandate, CancellationToken cancellationToken)
    {
        _db.DirectDebitMandates.Update(directDebitMandate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DirectDebitMandate directDebitMandate, CancellationToken cancellationToken)
    {
        _db.DirectDebitMandates.Remove(directDebitMandate);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
