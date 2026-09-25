
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class CollateralRepository : ICollateralRepository
{
    private readonly ApplicationDbContext _db;

    public CollateralRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Collateral?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Collaterals
            .Include(x => x.Loan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Collateral>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Collaterals
            .AsNoTracking()
            .Include(x => x.Loan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Collateral collateral, CancellationToken cancellationToken)
    {
        _db.Collaterals.Add(collateral);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Collateral collateral, CancellationToken cancellationToken)
    {
        _db.Collaterals.Update(collateral);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Collateral collateral, CancellationToken cancellationToken)
    {
        _db.Collaterals.Remove(collateral);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
