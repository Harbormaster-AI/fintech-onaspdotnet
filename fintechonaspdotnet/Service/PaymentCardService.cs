using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IPaymentCardService {

    Task Create(PaymentCard model , CancellationToken cancellationToken);
    Task<bool> Update(PaymentCard model, CancellationToken cancellationToken);
    Task<PaymentCard?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentCard>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAccount(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToTokenizations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTokenizations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PaymentCardService : IPaymentCardService
{
    private readonly IPaymentCardRepository _repository;
    private readonly ILogger<PaymentCardService> _logger;

    public PaymentCardService(
        IPaymentCardRepository repository, ILogger<PaymentCardService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PaymentCard model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PaymentCard model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.CardToken = model.CardToken;
            existing.MaskedPan = model.MaskedPan;
            existing.ExpiryMonth = model.ExpiryMonth;
            existing.ExpiryYear = model.ExpiryYear;
            existing.CardholderName = model.CardholderName;
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

    public Task<PaymentCard?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PaymentCard>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToTokenizations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTokenizations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
