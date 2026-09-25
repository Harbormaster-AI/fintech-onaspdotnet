
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IFeeScheduleRepository _repository;
    private readonly ILogger<FeeScheduleService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public FeeScheduleService(
        ApplicationTelemetry telemetry,
        IFeeScheduleRepository repository,
        ILogger<FeeScheduleService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(FeeSchedule model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "FeeSchedule",
                "CreateFeeSchedule",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "FeeSchedule",
                "UpdateFeeSchedule",
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
            await _telemetry.Execute(
                "FeeSchedule",
                "UpdateFeeSchedule",
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

    public async Task<bool> AssignPricingPlan(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No FeeSchedule found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<PricingPlanService>().Get(childRequest, cancellationToken);
            parent.PricingPlan = child;
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

    public async Task<bool> UnassignPricingPlan(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No FeeSchedule found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.PricingPlan = null;
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




}
