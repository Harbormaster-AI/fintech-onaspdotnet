
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class SecurityRepository : ISecurityRepository
{
    private readonly ApplicationDbContext _db;

    public SecurityRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Security?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Securitys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Security>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Securitys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Security security, CancellationToken cancellationToken)
    {
        _db.Securitys.Add(security);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Security security, CancellationToken cancellationToken)
    {
        _db.Securitys.Update(security);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Security security, CancellationToken cancellationToken)
    {
        _db.Securitys.Remove(security);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPositionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Positions
            .Where(position =>
                request.ChildIds.Contains(position.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    position =>
                        EF.Property<Guid?>(
                            position,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPositionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Positions
            .Where(position =>
                request.ChildIds.Contains(position.Id) &&
                EF.Property<Guid?>(
                    position,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    position =>
                        EF.Property<Guid?>(
                            position,
                            "ExchangeRate_Id"),
                    (Guid?)null));
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


    public async Task AddToOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TradeOrders
            .Where(tradeOrder =>
                request.ChildIds.Contains(tradeOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    tradeOrder =>
                        EF.Property<Guid?>(
                            tradeOrder,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TradeOrders
            .Where(tradeOrder =>
                request.ChildIds.Contains(tradeOrder.Id) &&
                EF.Property<Guid?>(
                    tradeOrder,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    tradeOrder =>
                        EF.Property<Guid?>(
                            tradeOrder,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
