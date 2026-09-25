
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class BeneficiaryRepository : IBeneficiaryRepository
{
    private readonly ApplicationDbContext _db;

    public BeneficiaryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Beneficiary?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Beneficiarys
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Beneficiary>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Beneficiarys
            .AsNoTracking()
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Beneficiary beneficiary, CancellationToken cancellationToken)
    {
        _db.Beneficiarys.Add(beneficiary);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Beneficiary beneficiary, CancellationToken cancellationToken)
    {
        _db.Beneficiarys.Update(beneficiary);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Beneficiary beneficiary, CancellationToken cancellationToken)
    {
        _db.Beneficiarys.Remove(beneficiary);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
