using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IProductOfferingRepository
{
    Task<ProductOffering?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductOffering>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProductOffering productOffering, CancellationToken cancellationToken);
    Task UpdateAsync(ProductOffering productOffering, CancellationToken cancellationToken);
    Task DeleteAsync(ProductOffering productOffering, CancellationToken cancellationToken);

    Task AddToPricingPlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPricingPlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
