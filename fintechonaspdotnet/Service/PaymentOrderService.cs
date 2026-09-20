using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IPaymentOrderService {

    Task Create(PaymentOrder model , CancellationToken cancellationToken);
    Task<bool> Update(PaymentOrder model, CancellationToken cancellationToken);
    Task<PaymentOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignSourceAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSourceAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDestinationAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDestinationAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignBeneficiary(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBeneficiary(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignFxDeal(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFxDeal(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToFees(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFees(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PaymentOrderService : IPaymentOrderService
{
    private readonly IPaymentOrderRepository _repository;
    private readonly ILogger<PaymentOrderService> _logger;

    public PaymentOrderService(
        IPaymentOrderRepository repository, ILogger<PaymentOrderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PaymentOrder model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PaymentOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.OrderReference = model.OrderReference;
            existing.RequestedExecutionDate = model.RequestedExecutionDate;
            existing.Purpose = model.Purpose;
            existing.PaymentMethod = model.PaymentMethod;
            existing.Status = model.Status;
            existing.Priority = model.Priority;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<PaymentOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PaymentOrder>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignSourceAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSourceAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignDestinationAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDestinationAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignBeneficiary(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignBeneficiary(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignFxDeal(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignFxDeal(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToFees(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromFees(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
