
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class CompliancePolicyRepository : ICompliancePolicyRepository
{
    private readonly ApplicationDbContext _db;

    public CompliancePolicyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CompliancePolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CompliancePolicys
            .Include(x => x.Institution)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CompliancePolicy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CompliancePolicys
            .AsNoTracking()
            .Include(x => x.Institution)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CompliancePolicy compliancePolicy, CancellationToken cancellationToken)
    {
        _db.CompliancePolicys.Add(compliancePolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CompliancePolicy compliancePolicy, CancellationToken cancellationToken)
    {
        _db.CompliancePolicys.Update(compliancePolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CompliancePolicy compliancePolicy, CancellationToken cancellationToken)
    {
        _db.CompliancePolicys.Remove(compliancePolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
