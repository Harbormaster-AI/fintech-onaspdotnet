using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IInvestmentPortfolioRepository
{
    Task<InvestmentPortfolio?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InvestmentPortfolio>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InvestmentPortfolio investmentPortfolio, CancellationToken cancellationToken);
    Task UpdateAsync(InvestmentPortfolio investmentPortfolio, CancellationToken cancellationToken);
    Task DeleteAsync(InvestmentPortfolio investmentPortfolio, CancellationToken cancellationToken);
}
