using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class KYCProfileRepository : IKYCProfileRepository
{
    private readonly ApplicationDbContext _db;

    public KYCProfileRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<KYCProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.KYCProfiles
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<KYCProfile>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.KYCProfiles
            .AsNoTracking()
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(KYCProfile kYCProfile, CancellationToken cancellationToken)
    {
        _db.KYCProfiles.Add(kYCProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(KYCProfile kYCProfile, CancellationToken cancellationToken)
    {
        _db.KYCProfiles.Update(kYCProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(KYCProfile kYCProfile, CancellationToken cancellationToken)
    {
        _db.KYCProfiles.Remove(kYCProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
