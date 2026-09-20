using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IPaymentContractService {

    Task Create(PaymentContract model , CancellationToken cancellationToken);
    Task<bool> Update(PaymentContract model, CancellationToken cancellationToken);
    Task<PaymentContract?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentContract>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAcquirer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAcquirer(AssociationRequest request, CancellationToken cancellationToken);


}

public class PaymentContractService : IPaymentContractService
{
    private readonly IPaymentContractRepository _repository;
    private readonly ILogger<PaymentContractService> _logger;

    public PaymentContractService(
        IPaymentContractRepository repository, ILogger<PaymentContractService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PaymentContract model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PaymentContract model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ContractNumber = model.ContractNumber;
            existing.PricingPlanCode = model.PricingPlanCode;
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

    public Task<PaymentContract?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PaymentContract>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignAcquirer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAcquirer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
