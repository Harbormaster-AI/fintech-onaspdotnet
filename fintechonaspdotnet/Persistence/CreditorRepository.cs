
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class CreditorRepository : ICreditorRepository
{
    private readonly ApplicationDbContext _db;

    public CreditorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Creditor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Creditors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Creditor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Creditors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Creditor creditor, CancellationToken cancellationToken)
    {
        _db.Creditors.Add(creditor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Creditor creditor, CancellationToken cancellationToken)
    {
        _db.Creditors.Update(creditor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Creditor creditor, CancellationToken cancellationToken)
    {
        _db.Creditors.Remove(creditor);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToMandatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DirectDebitMandates
            .Where(directDebitMandate =>
                request.ChildIds.Contains(directDebitMandate.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    directDebitMandate =>
                        EF.Property<Guid?>(
                            directDebitMandate,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromMandatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DirectDebitMandates
            .Where(directDebitMandate =>
                request.ChildIds.Contains(directDebitMandate.Id) &&
                EF.Property<Guid?>(
                    directDebitMandate,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    directDebitMandate =>
                        EF.Property<Guid?>(
                            directDebitMandate,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
