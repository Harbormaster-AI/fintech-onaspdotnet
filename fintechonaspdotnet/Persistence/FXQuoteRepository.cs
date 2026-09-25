
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class FXQuoteRepository : IFXQuoteRepository
{
    private readonly ApplicationDbContext _db;

    public FXQuoteRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<FXQuote?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.FXQuotes
            .Include(x => x.RequestedBy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FXQuote>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.FXQuotes
            .AsNoTracking()
            .Include(x => x.RequestedBy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FXQuote fXQuote, CancellationToken cancellationToken)
    {
        _db.FXQuotes.Add(fXQuote);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FXQuote fXQuote, CancellationToken cancellationToken)
    {
        _db.FXQuotes.Update(fXQuote);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FXQuote fXQuote, CancellationToken cancellationToken)
    {
        _db.FXQuotes.Remove(fXQuote);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
