using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IPricingPlanService {

    Task Create(PricingPlan model , CancellationToken cancellationToken);
    Task<bool> Update(PricingPlan model, CancellationToken cancellationToken);
    Task<PricingPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PricingPlan>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignProductOffering(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProductOffering(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToFeeSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFeeSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLimits(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLimits(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PricingPlanService : IPricingPlanService
{
    private readonly IPricingPlanRepository _repository;
    private readonly ILogger<PricingPlanService> _logger;

    public PricingPlanService(
        IPricingPlanRepository repository, ILogger<PricingPlanService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PricingPlan model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PricingPlan model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.PlanCode = model.PlanCode;
            existing.BaseCurrency = model.BaseCurrency;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<PricingPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PricingPlan>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignProductOffering(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignProductOffering(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToFeeSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromFeeSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLimits(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLimits(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
