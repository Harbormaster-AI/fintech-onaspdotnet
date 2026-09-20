using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IComplianceAlertService {

    Task Create(ComplianceAlert model , CancellationToken cancellationToken);
    Task<bool> Update(ComplianceAlert model, CancellationToken cancellationToken);
    Task<ComplianceAlert?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ComplianceAlert>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignScreening(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignScreening(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTransaction(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTransaction(AssociationRequest request, CancellationToken cancellationToken);


}

public class ComplianceAlertService : IComplianceAlertService
{
    private readonly IComplianceAlertRepository _repository;
    private readonly ILogger<ComplianceAlertService> _logger;

    public ComplianceAlertService(
        IComplianceAlertRepository repository, ILogger<ComplianceAlertService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ComplianceAlert model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ComplianceAlert model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.AlertCode = model.AlertCode;
            existing.RaisedAt = model.RaisedAt;
            existing.Notes = model.Notes;
            existing.Severity = model.Severity;
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

    public Task<ComplianceAlert?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ComplianceAlert>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignScreening(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignScreening(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTransaction(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTransaction(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
