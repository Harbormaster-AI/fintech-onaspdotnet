using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IFinancialInstitutionRepository
{
    Task<FinancialInstitution?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FinancialInstitution>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken);
    Task UpdateAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken);
    Task DeleteAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken);

    Task AddToBranchesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBranchesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCustomersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCustomersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToProductOfferingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProductOfferingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPaymentProcessorsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentProcessorsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCompliancePoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCompliancePoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
