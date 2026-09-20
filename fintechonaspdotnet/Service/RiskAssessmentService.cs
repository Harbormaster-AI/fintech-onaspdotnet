using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

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
    private readonly IRiskAssessmentRepository _repository;
    private readonly ILogger<RiskAssessmentService> _logger;

    public RiskAssessmentService(
        IRiskAssessmentRepository repository, ILogger<RiskAssessmentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(RiskAssessment model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignApplication(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignApplication(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
