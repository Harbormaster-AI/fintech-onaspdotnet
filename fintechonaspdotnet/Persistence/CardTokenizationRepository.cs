
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class CardTokenizationRepository : ICardTokenizationRepository
{
    private readonly ApplicationDbContext _db;

    public CardTokenizationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CardTokenization?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CardTokenizations
            .Include(x => x.Card)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CardTokenization>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CardTokenizations
            .AsNoTracking()
            .Include(x => x.Card)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CardTokenization cardTokenization, CancellationToken cancellationToken)
    {
        _db.CardTokenizations.Add(cardTokenization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CardTokenization cardTokenization, CancellationToken cancellationToken)
    {
        _db.CardTokenizations.Update(cardTokenization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CardTokenization cardTokenization, CancellationToken cancellationToken)
    {
        _db.CardTokenizations.Remove(cardTokenization);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
