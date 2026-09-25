using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IInvestmentAccountRepository
{
    Task<InvestmentAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InvestmentAccount>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken);
    Task UpdateAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken);
    Task DeleteAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken);

    Task AddToTradesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTradesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
