using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IInvestmentAccountRepository
{
    Task<InvestmentAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InvestmentAccount>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken);
    Task UpdateAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken);
    Task DeleteAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken);
}
