
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class DisputeRepository : IDisputeRepository
{
    private readonly ApplicationDbContext _db;

    public DisputeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Dispute?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Disputes
            .Include(x => x.Transaction)
            .Include(x => x.Card)
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Dispute>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Disputes
            .AsNoTracking()
            .Include(x => x.Transaction)
            .Include(x => x.Card)
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Dispute dispute, CancellationToken cancellationToken)
    {
        _db.Disputes.Add(dispute);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Dispute dispute, CancellationToken cancellationToken)
    {
        _db.Disputes.Update(dispute);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Dispute dispute, CancellationToken cancellationToken)
    {
        _db.Disputes.Remove(dispute);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToChargebacksAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Chargebacks
            .Where(chargeback =>
                request.ChildIds.Contains(chargeback.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    chargeback =>
                        EF.Property<Guid?>(
                            chargeback,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromChargebacksAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Chargebacks
            .Where(chargeback =>
                request.ChildIds.Contains(chargeback.Id) &&
                EF.Property<Guid?>(
                    chargeback,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    chargeback =>
                        EF.Property<Guid?>(
                            chargeback,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
