using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IPaymentProcessorService {

    Task Create(PaymentProcessor model , CancellationToken cancellationToken);
    Task<bool> Update(PaymentProcessor model, CancellationToken cancellationToken);
    Task<PaymentProcessor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentProcessor>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToInstitutions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInstitutions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSettlements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSettlements(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PaymentProcessorService : IPaymentProcessorService
{
    private readonly IPaymentProcessorRepository _repository;
    private readonly ILogger<PaymentProcessorService> _logger;

    public PaymentProcessorService(
        IPaymentProcessorRepository repository, ILogger<PaymentProcessorService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PaymentProcessor model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PaymentProcessor model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.ProcessorCode = model.ProcessorCode;
            existing.NetworkSupport = model.NetworkSupport;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<PaymentProcessor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PaymentProcessor>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToInstitutions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInstitutions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToSettlements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSettlements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
