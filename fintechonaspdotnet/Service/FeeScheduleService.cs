using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IFeeScheduleService {

    Task Create(FeeSchedule model , CancellationToken cancellationToken);
    Task<bool> Update(FeeSchedule model, CancellationToken cancellationToken);
    Task<FeeSchedule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<FeeSchedule>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPricingPlan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPricingPlan(AssociationRequest request, CancellationToken cancellationToken);


}

public class FeeScheduleService : IFeeScheduleService
{
    private readonly IFeeScheduleRepository _repository;
    private readonly ILogger<FeeScheduleService> _logger;

    public FeeScheduleService(
        IFeeScheduleRepository repository, ILogger<FeeScheduleService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(FeeSchedule model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(FeeSchedule model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Amount = model.Amount;
            existing.Percentage = model.Percentage;
            existing.Minimum = model.Minimum;
            existing.Maximum = model.Maximum;
            existing.FeeType = model.FeeType;
            existing.CalculationMethod = model.CalculationMethod;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<FeeSchedule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<FeeSchedule>> GetAll(CancellationToken cancellationToken)
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
