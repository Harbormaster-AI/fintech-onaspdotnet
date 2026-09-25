
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class VerifiedAddressRepository : IVerifiedAddressRepository
{
    private readonly ApplicationDbContext _db;

    public VerifiedAddressRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<VerifiedAddress?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.VerifiedAddresss
            .Include(x => x.KycProfile)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<VerifiedAddress>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.VerifiedAddresss
            .AsNoTracking()
            .Include(x => x.KycProfile)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(VerifiedAddress verifiedAddress, CancellationToken cancellationToken)
    {
        _db.VerifiedAddresss.Add(verifiedAddress);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(VerifiedAddress verifiedAddress, CancellationToken cancellationToken)
    {
        _db.VerifiedAddresss.Update(verifiedAddress);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(VerifiedAddress verifiedAddress, CancellationToken cancellationToken)
    {
        _db.VerifiedAddresss.Remove(verifiedAddress);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
