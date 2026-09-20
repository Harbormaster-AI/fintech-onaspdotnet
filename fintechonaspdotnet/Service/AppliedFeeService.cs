using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IAppliedFeeService {

    Task Create(AppliedFee model , CancellationToken cancellationToken);
    Task<bool> Update(AppliedFee model, CancellationToken cancellationToken);
    Task<AppliedFee?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AppliedFee>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPaymentOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPaymentOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTransaction(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTransaction(AssociationRequest request, CancellationToken cancellationToken);


}

public class AppliedFeeService : IAppliedFeeService
{
    private readonly IAppliedFeeRepository _repository;
    private readonly ILogger<AppliedFeeService> _logger;

    public AppliedFeeService(
        IAppliedFeeRepository repository, ILogger<AppliedFeeService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AppliedFee model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(AppliedFee model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Amount = model.Amount;
            existing.Description = model.Description;
            existing.FeeType = model.FeeType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<AppliedFee?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AppliedFee>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPaymentOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPaymentOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTransaction(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTransaction(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
