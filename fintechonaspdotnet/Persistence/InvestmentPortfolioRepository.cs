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
}
