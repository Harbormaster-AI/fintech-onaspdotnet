using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Customer customer, CancellationToken cancellationToken);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken);
    Task DeleteAsync(Customer customer, CancellationToken cancellationToken);

    Task AddToAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAccountsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToWalletsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWalletsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCardsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCardsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToKycProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromKycProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToConsentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromConsentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAgreementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAgreementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLoanApplicationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLoanApplicationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLoansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLoansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPortfoliosAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPortfoliosAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDisputesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDisputesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
