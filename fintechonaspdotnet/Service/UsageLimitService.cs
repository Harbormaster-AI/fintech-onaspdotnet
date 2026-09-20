using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IUsageLimitService {

    Task Create(UsageLimit model , CancellationToken cancellationToken);
    Task<bool> Update(UsageLimit model, CancellationToken cancellationToken);
    Task<UsageLimit?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<UsageLimit>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPricingPlan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPricingPlan(AssociationRequest request, CancellationToken cancellationToken);


}

public class UsageLimitService : IUsageLimitService
{
    private readonly IUsageLimitRepository _repository;
    private readonly ILogger<UsageLimitService> _logger;

    public UsageLimitService(
        IUsageLimitRepository repository, ILogger<UsageLimitService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(UsageLimit model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(UsageLimit model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Amount = model.Amount;
            existing.Count = model.Count;
            existing.Scope = model.Scope;
            existing.Period = model.Period;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<UsageLimit?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<UsageLimit>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPricingPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPricingPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
