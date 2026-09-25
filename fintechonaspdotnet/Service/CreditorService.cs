
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Telemetry;

namespace fintechonaspdotnet.Service;

public interface ICreditorService {

    Task Create(Creditor model , CancellationToken cancellationToken);
    Task<bool> Update(Creditor model, CancellationToken cancellationToken);
    Task<Creditor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Creditor>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToMandates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMandates(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CreditorService : ICreditorService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICreditorRepository _repository;
    private readonly ILogger<CreditorService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CreditorService(
        ApplicationTelemetry telemetry,
        ICreditorRepository repository,
        ILogger<CreditorService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Creditor model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Creditor",
                "CreateCreditor",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Creditor model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Bic = model.Bic;
            existing.Address = model.Address;

            await _telemetry.Execute(
                "Creditor",
                "UpdateCreditor",
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

    public Task<Creditor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Creditor>> GetAll(CancellationToken cancellationToken)
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
                "Creditor",
                "UpdateCreditor",
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


    public async Task<bool> AddToMandates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Creditor",
                "AddToMandates",
                () => _repository.AddToMandatesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromMandates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Creditor",
                "RemoveFromMandates",
                () => _repository.RemoveFromMandatesAsync(request, cancellationToken));
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
