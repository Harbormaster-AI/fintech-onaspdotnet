using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface ITransactionService {

    Task Create(Transaction model , CancellationToken cancellationToken);
    Task<bool> Update(Transaction model, CancellationToken cancellationToken);
    Task<Transaction?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Transaction>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWallet(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWallet(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPaymentOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPaymentOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCard(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCard(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToRelatedTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRelatedTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(
        ITransactionRepository repository, ILogger<TransactionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Transaction model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Transaction model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Amount = model.Amount;
            existing.Fee = model.Fee;
            existing.ExchangeRate = model.ExchangeRate;
            existing.CreatedAt = model.CreatedAt;
            existing.CompletedAt = model.CompletedAt;
            existing.Narrative = model.Narrative;
            existing.TransactionType = model.TransactionType;
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

    public Task<Transaction?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Transaction>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignWallet(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWallet(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPaymentOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPaymentOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignMerchant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignMerchant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCard(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCard(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToRelatedTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromRelatedTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
