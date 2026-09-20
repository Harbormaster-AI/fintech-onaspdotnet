using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class MerchantRepository : IMerchantRepository
{
    private readonly ApplicationDbContext _db;

    public MerchantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Merchant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Merchants
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Merchant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Merchants
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Merchant merchant, CancellationToken cancellationToken)
    {
        _db.Merchants.Add(merchant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Merchant merchant, CancellationToken cancellationToken)
    {
        _db.Merchants.Update(merchant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Merchant merchant, CancellationToken cancellationToken)
    {
        _db.Merchants.Remove(merchant);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
