
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class InvestmentAccountRepository : IInvestmentAccountRepository
{
    private readonly ApplicationDbContext _db;

    public InvestmentAccountRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InvestmentAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InvestmentAccounts
            .Include(x => x.Portfolio)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InvestmentAccount>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InvestmentAccounts
            .AsNoTracking()
            .Include(x => x.Portfolio)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken)
    {
        _db.InvestmentAccounts.Add(investmentAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken)
    {
        _db.InvestmentAccounts.Update(investmentAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken)
    {
        _db.InvestmentAccounts.Remove(investmentAccount);
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
