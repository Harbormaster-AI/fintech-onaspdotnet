using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IPayoutService {

    Task Create(Payout model , CancellationToken cancellationToken);
    Task<bool> Update(Payout model, CancellationToken cancellationToken);
    Task<Payout?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Payout>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSettlementBatch(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSettlementBatch(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDestinationAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDestinationAccount(AssociationRequest request, CancellationToken cancellationToken);


}

public class PayoutService : IPayoutService
{
    private readonly IPayoutRepository _repository;
    private readonly ILogger<PayoutService> _logger;

    public PayoutService(
        IPayoutRepository repository, ILogger<PayoutService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Payout model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Payout model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PayoutReference = model.PayoutReference;
            existing.Amount = model.Amount;
            existing.Currency = model.Currency;
            existing.ScheduledDate = model.ScheduledDate;
            existing.PaidDate = model.PaidDate;
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

    public Task<Payout?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Payout>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignSettlementBatch(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSettlementBatch(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignDestinationAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDestinationAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
