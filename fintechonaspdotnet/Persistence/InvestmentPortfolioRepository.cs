
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class InvestmentPortfolioRepository : IInvestmentPortfolioRepository
{
    private readonly ApplicationDbContext _db;

    public InvestmentPortfolioRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InvestmentPortfolio?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InvestmentPortfolios
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InvestmentPortfolio>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InvestmentPortfolios
            .AsNoTracking()
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InvestmentPortfolio investmentPortfolio, CancellationToken cancellationToken)
    {
        _db.InvestmentPortfolios.Add(investmentPortfolio);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InvestmentPortfolio investmentPortfolio, CancellationToken cancellationToken)
    {
        _db.InvestmentPortfolios.Update(investmentPortfolio);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InvestmentPortfolio investmentPortfolio, CancellationToken cancellationToken)
    {
        _db.InvestmentPortfolios.Remove(investmentPortfolio);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAccountsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InvestmentAccounts
            .Where(investmentAccount =>
                request.ChildIds.Contains(investmentAccount.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    investmentAccount =>
                        EF.Property<Guid?>(
                            investmentAccount,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAccountsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InvestmentAccounts
            .Where(investmentAccount =>
                request.ChildIds.Contains(investmentAccount.Id) &&
                EF.Property<Guid?>(
                    investmentAccount,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    investmentAccount =>
                        EF.Property<Guid?>(
                            investmentAccount,
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


    public async Task AddToHoldingsAsync(
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

    public async Task RemoveFromHoldingsAsync(
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

}
