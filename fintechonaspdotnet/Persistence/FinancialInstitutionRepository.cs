using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class FinancialInstitutionRepository : IFinancialInstitutionRepository
{
    private readonly ApplicationDbContext _db;

    public FinancialInstitutionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<FinancialInstitution?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.FinancialInstitutions
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FinancialInstitution>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.FinancialInstitutions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken)
    {
        _db.FinancialInstitutions.Add(financialInstitution);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken)
    {
        _db.FinancialInstitutions.Update(financialInstitution);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken)
    {
        _db.FinancialInstitutions.Remove(financialInstitution);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
