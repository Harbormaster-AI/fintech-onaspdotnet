
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class BranchRepository : IBranchRepository
{
    private readonly ApplicationDbContext _db;

    public BranchRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Branchs
            .Include(x => x.Institution)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Branch>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Branchs
            .AsNoTracking()
            .Include(x => x.Institution)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Branch branch, CancellationToken cancellationToken)
    {
        _db.Branchs.Add(branch);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Branch branch, CancellationToken cancellationToken)
    {
        _db.Branchs.Update(branch);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Branch branch, CancellationToken cancellationToken)
    {
        _db.Branchs.Remove(branch);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
