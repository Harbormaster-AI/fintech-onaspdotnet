using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IProductOfferingService {

    Task Create(ProductOffering model , CancellationToken cancellationToken);
    Task<bool> Update(ProductOffering model, CancellationToken cancellationToken);
    Task<ProductOffering?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductOffering>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignInstitution(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInstitution(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPricingPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPricingPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ProductOfferingService : IProductOfferingService
{
    private readonly IProductOfferingRepository _repository;
    private readonly ILogger<ProductOfferingService> _logger;

    public ProductOfferingService(
        IProductOfferingRepository repository, ILogger<ProductOfferingService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ProductOffering model, CancellationToken cancellationToken)
    {

         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(ProductOffering model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.ProductCode = model.ProductCode;
            existing.Category = model.Category;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ProductOffering?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ProductOffering>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignInstitution(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInstitution(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToPricingPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPricingPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
