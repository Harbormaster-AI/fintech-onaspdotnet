using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IChargebackService {

    Task Create(Chargeback model , CancellationToken cancellationToken);
    Task<bool> Update(Chargeback model, CancellationToken cancellationToken);
    Task<Chargeback?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Chargeback>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDispute(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDispute(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTransaction(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTransaction(AssociationRequest request, CancellationToken cancellationToken);


}

public class ChargebackService : IChargebackService
{
    private readonly IChargebackRepository _repository;
    private readonly ILogger<ChargebackService> _logger;

    public ChargebackService(
        IChargebackRepository repository, ILogger<ChargebackService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Chargeback model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Chargeback model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ChargebackReference = model.ChargebackReference;
            existing.Amount = model.Amount;
            existing.PostedAt = model.PostedAt;
            existing.Stage = model.Stage;
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

    public Task<Chargeback?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Chargeback>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignDispute(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDispute(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTransaction(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTransaction(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
