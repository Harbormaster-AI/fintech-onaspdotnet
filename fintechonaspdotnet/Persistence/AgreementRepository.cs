
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class AgreementRepository : IAgreementRepository
{
    private readonly ApplicationDbContext _db;

    public AgreementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Agreement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Agreements
            .Include(x => x.Customer)
            .Include(x => x.ProductOffering)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Agreement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Agreements
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.ProductOffering)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Agreement agreement, CancellationToken cancellationToken)
    {
        _db.Agreements.Add(agreement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Agreement agreement, CancellationToken cancellationToken)
    {
        _db.Agreements.Update(agreement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Agreement agreement, CancellationToken cancellationToken)
    {
        _db.Agreements.Remove(agreement);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
