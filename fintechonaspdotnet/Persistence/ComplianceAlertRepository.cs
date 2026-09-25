
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class ComplianceAlertRepository : IComplianceAlertRepository
{
    private readonly ApplicationDbContext _db;

    public ComplianceAlertRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ComplianceAlert?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ComplianceAlerts
            .Include(x => x.Screening)
            .Include(x => x.Transaction)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ComplianceAlert>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ComplianceAlerts
            .AsNoTracking()
            .Include(x => x.Screening)
            .Include(x => x.Transaction)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ComplianceAlert complianceAlert, CancellationToken cancellationToken)
    {
        _db.ComplianceAlerts.Add(complianceAlert);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ComplianceAlert complianceAlert, CancellationToken cancellationToken)
    {
        _db.ComplianceAlerts.Update(complianceAlert);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ComplianceAlert complianceAlert, CancellationToken cancellationToken)
    {
        _db.ComplianceAlerts.Remove(complianceAlert);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
