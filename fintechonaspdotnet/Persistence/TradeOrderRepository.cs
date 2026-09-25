
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class TradeOrderRepository : ITradeOrderRepository
{
    private readonly ApplicationDbContext _db;

    public TradeOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TradeOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TradeOrders
            .Include(x => x.Portfolio)
            .Include(x => x.Security)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TradeOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TradeOrders
            .AsNoTracking()
            .Include(x => x.Portfolio)
            .Include(x => x.Security)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TradeOrder tradeOrder, CancellationToken cancellationToken)
    {
        _db.TradeOrders.Add(tradeOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TradeOrder tradeOrder, CancellationToken cancellationToken)
    {
        _db.TradeOrders.Update(tradeOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TradeOrder tradeOrder, CancellationToken cancellationToken)
    {
        _db.TradeOrders.Remove(tradeOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToTradesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Trades
            .Where(trade =>
                request.ChildIds.Contains(trade.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    trade =>
                        EF.Property<Guid?>(
                            trade,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTradesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Trades
            .Where(trade =>
                request.ChildIds.Contains(trade.Id) &&
                EF.Property<Guid?>(
                    trade,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    trade =>
                        EF.Property<Guid?>(
                            trade,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
