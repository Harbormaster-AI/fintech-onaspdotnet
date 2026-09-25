
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly ApplicationDbContext _db;

    public ExchangeRateRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ExchangeRate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ExchangeRates
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ExchangeRate>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ExchangeRates
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken)
    {
        _db.ExchangeRates.Add(exchangeRate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken)
    {
        _db.ExchangeRates.Update(exchangeRate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken)
    {
        _db.ExchangeRates.Remove(exchangeRate);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToUsedByQuotesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FXQuotes
            .Where(fXQuote =>
                request.ChildIds.Contains(fXQuote.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    fXQuote =>
                        EF.Property<Guid?>(
                            fXQuote,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromUsedByQuotesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FXQuotes
            .Where(fXQuote =>
                request.ChildIds.Contains(fXQuote.Id) &&
                EF.Property<Guid?>(
                    fXQuote,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    fXQuote =>
                        EF.Property<Guid?>(
                            fXQuote,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
