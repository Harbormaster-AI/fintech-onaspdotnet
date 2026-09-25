
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class TradeRepository : ITradeRepository
{
    private readonly ApplicationDbContext _db;

    public TradeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Trade?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Trades
            .Include(x => x.Order)
            .Include(x => x.Security)
            .Include(x => x.InvestmentAccount)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Trade>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Trades
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.Security)
            .Include(x => x.InvestmentAccount)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Trade trade, CancellationToken cancellationToken)
    {
        _db.Trades.Add(trade);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Trade trade, CancellationToken cancellationToken)
    {
        _db.Trades.Update(trade);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Trade trade, CancellationToken cancellationToken)
    {
        _db.Trades.Remove(trade);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
