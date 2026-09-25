
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IProductOfferingRepository _repository;
    private readonly ILogger<ProductOfferingService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ProductOfferingService(
        ApplicationTelemetry telemetry,
        IProductOfferingRepository repository,
        ILogger<ProductOfferingService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ProductOffering model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ProductOffering",
                "CreateProductOffering",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "ProductOffering",
                "UpdateProductOffering",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "ProductOffering",
                "UpdateProductOffering",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignInstitution(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ProductOffering found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<FinancialInstitutionService>().Get(childRequest, cancellationToken);
            parent.Institution = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignInstitution(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ProductOffering found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Institution = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToPricingPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductOffering",
                "AddToPricingPlans",
                () => _repository.AddToPricingPlansAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromPricingPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ProductOffering",
                "RemoveFromPricingPlans",
                () => _repository.RemoveFromPricingPlansAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
