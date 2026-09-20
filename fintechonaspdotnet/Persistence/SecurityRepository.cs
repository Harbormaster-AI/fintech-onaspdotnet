using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class SecurityRepository : ISecurityRepository
{
    private readonly ApplicationDbContext _db;

    public SecurityRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Security?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Securitys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Security>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Securitys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Security security, CancellationToken cancellationToken)
    {
        _db.Securitys.Add(security);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Security security, CancellationToken cancellationToken)
    {
        _db.Securitys.Update(security);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Security security, CancellationToken cancellationToken)
    {
        _db.Securitys.Remove(security);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
