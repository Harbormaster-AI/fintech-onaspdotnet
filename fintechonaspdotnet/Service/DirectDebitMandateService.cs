using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IDirectDebitMandateService {

    Task Create(DirectDebitMandate model , CancellationToken cancellationToken);
    Task<bool> Update(DirectDebitMandate model, CancellationToken cancellationToken);
    Task<DirectDebitMandate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DirectDebitMandate>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCreditor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCreditor(AssociationRequest request, CancellationToken cancellationToken);


}

public class DirectDebitMandateService : IDirectDebitMandateService
{
    private readonly IDirectDebitMandateRepository _repository;
    private readonly ILogger<DirectDebitMandateService> _logger;

    public DirectDebitMandateService(
        IDirectDebitMandateRepository repository, ILogger<DirectDebitMandateService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(DirectDebitMandate model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(DirectDebitMandate model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.MandateId = model.MandateId;
            existing.SignedAt = model.SignedAt;
            existing.Scheme = model.Scheme;
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

    public Task<DirectDebitMandate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DirectDebitMandate>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCreditor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCreditor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
