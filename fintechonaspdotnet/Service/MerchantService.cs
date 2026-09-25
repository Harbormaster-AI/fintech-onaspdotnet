
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IMerchantRepository _repository;
    private readonly ILogger<MerchantService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public MerchantService(
        ApplicationTelemetry telemetry,
        IMerchantRepository repository,
        ILogger<MerchantService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Merchant model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Merchant",
                "CreateMerchant",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "Merchant",
                "UpdateMerchant",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "Merchant",
                "UpdateMerchant",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToTerminals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "AddToTerminals",
                () => _repository.AddToTerminalsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromTerminals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "RemoveFromTerminals",
                () => _repository.RemoveFromTerminalsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToPaymentContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "AddToPaymentContracts",
                () => _repository.AddToPaymentContractsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromPaymentContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "RemoveFromPaymentContracts",
                () => _repository.RemoveFromPaymentContractsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToPayouts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "AddToPayouts",
                () => _repository.AddToPayoutsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromPayouts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "RemoveFromPayouts",
                () => _repository.RemoveFromPayoutsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToSettlements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "AddToSettlements",
                () => _repository.AddToSettlementsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromSettlements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "RemoveFromSettlements",
                () => _repository.RemoveFromSettlementsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "AddToDisputes",
                () => _repository.AddToDisputesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "RemoveFromDisputes",
                () => _repository.RemoveFromDisputesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToInvoices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "AddToInvoices",
                () => _repository.AddToInvoicesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromInvoices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Merchant",
                "RemoveFromInvoices",
                () => _repository.RemoveFromInvoicesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
