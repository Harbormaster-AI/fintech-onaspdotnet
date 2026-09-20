using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IMerchantService {

    Task Create(Merchant model , CancellationToken cancellationToken);
    Task<bool> Update(Merchant model, CancellationToken cancellationToken);
    Task<Merchant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Merchant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToTerminals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTerminals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPaymentContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPaymentContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPayouts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPayouts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSettlements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSettlements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInvoices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInvoices(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class MerchantService : IMerchantService
{
    private readonly IMerchantRepository _repository;
    private readonly ILogger<MerchantService> _logger;

    public MerchantService(
        IMerchantRepository repository, ILogger<MerchantService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Merchant model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Merchant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Mcc = model.Mcc;
            existing.Url = model.Url;
            existing.Country = model.Country;
            existing.SettlementCurrency = model.SettlementCurrency;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Merchant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Merchant>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToTerminals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTerminals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPaymentContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPaymentContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPayouts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPayouts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToSettlements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSettlements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToInvoices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInvoices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
