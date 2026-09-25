using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IInvestmentPortfolioRepository
{
    Task<InvestmentPortfolio?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InvestmentPortfolio>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InvestmentPortfolio investmentPortfolio, CancellationToken cancellationToken);
    Task UpdateAsync(InvestmentPortfolio investmentPortfolio, CancellationToken cancellationToken);
    Task DeleteAsync(InvestmentPortfolio investmentPortfolio, CancellationToken cancellationToken);

    Task AddToAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToHoldingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromHoldingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
