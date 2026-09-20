using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface ISettlementBatchService {

    Task Create(SettlementBatch model , CancellationToken cancellationToken);
    Task<bool> Update(SettlementBatch model, CancellationToken cancellationToken);
    Task<SettlementBatch?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SettlementBatch>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignProcessor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProcessor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPayouts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPayouts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class SettlementBatchService : ISettlementBatchService
{
    private readonly ISettlementBatchRepository _repository;
    private readonly ILogger<SettlementBatchService> _logger;

    public SettlementBatchService(
        ISettlementBatchRepository repository, ILogger<SettlementBatchService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(SettlementBatch model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(SettlementBatch model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.BatchId = model.BatchId;
            existing.PeriodStart = model.PeriodStart;
            existing.PeriodEnd = model.PeriodEnd;
            existing.TotalVolume = model.TotalVolume;
            existing.TotalCount = model.TotalCount;
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

    public Task<SettlementBatch?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SettlementBatch>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignProcessor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignProcessor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToPayouts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPayouts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
