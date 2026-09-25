
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Telemetry;

namespace fintechonaspdotnet.Service;

public interface IRiskAssessmentService {

    Task Create(RiskAssessment model , CancellationToken cancellationToken);
    Task<bool> Update(RiskAssessment model, CancellationToken cancellationToken);
    Task<RiskAssessment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<RiskAssessment>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignApplication(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignApplication(AssociationRequest request, CancellationToken cancellationToken);


}

public class RiskAssessmentService : IRiskAssessmentService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IRiskAssessmentRepository _repository;
    private readonly ILogger<RiskAssessmentService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public RiskAssessmentService(
        ApplicationTelemetry telemetry,
        IRiskAssessmentRepository repository,
        ILogger<RiskAssessmentService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(RiskAssessment model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "RiskAssessment",
                "CreateRiskAssessment",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(RiskAssessment model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Score = model.Score;
            existing.AssessedAt = model.AssessedAt;
            existing.ModelVersion = model.ModelVersion;
            existing.Notes = model.Notes;
            existing.Decision = model.Decision;

            await _telemetry.Execute(
                "RiskAssessment",
                "UpdateRiskAssessment",
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

    public Task<RiskAssessment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<RiskAssessment>> GetAll(CancellationToken cancellationToken)
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
                "RiskAssessment",
                "UpdateRiskAssessment",
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

    public async Task<bool> AssignApplication(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No RiskAssessment found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<LoanApplicationService>().Get(childRequest, cancellationToken);
            parent.Application = child;
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

    public async Task<bool> UnassignApplication(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No RiskAssessment found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Application = null;
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
