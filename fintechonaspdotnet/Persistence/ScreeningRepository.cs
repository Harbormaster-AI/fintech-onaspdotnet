
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class ScreeningRepository : IScreeningRepository
{
    private readonly ApplicationDbContext _db;

    public ScreeningRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Screening?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Screenings
            .Include(x => x.KycProfile)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Screening>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Screenings
            .AsNoTracking()
            .Include(x => x.KycProfile)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Screening screening, CancellationToken cancellationToken)
    {
        _db.Screenings.Add(screening);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Screening screening, CancellationToken cancellationToken)
    {
        _db.Screenings.Update(screening);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Screening screening, CancellationToken cancellationToken)
    {
        _db.Screenings.Remove(screening);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAlertsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ComplianceAlerts
            .Where(complianceAlert =>
                request.ChildIds.Contains(complianceAlert.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    complianceAlert =>
                        EF.Property<Guid?>(
                            complianceAlert,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAlertsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ComplianceAlerts
            .Where(complianceAlert =>
                request.ChildIds.Contains(complianceAlert.Id) &&
                EF.Property<Guid?>(
                    complianceAlert,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    complianceAlert =>
                        EF.Property<Guid?>(
                            complianceAlert,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
