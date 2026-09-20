using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface ISecurityService {

    Task Create(Security model , CancellationToken cancellationToken);
    Task<bool> Update(Security model, CancellationToken cancellationToken);
    Task<Security?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Security>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToPositions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPositions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTrades(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTrades(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class SecurityService : ISecurityService
{
    private readonly ISecurityRepository _repository;
    private readonly ILogger<SecurityService> _logger;

    public SecurityService(
        ISecurityRepository repository, ILogger<SecurityService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Security model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Security model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Symbol = model.Symbol;
            existing.Isin = model.Isin;
            existing.Cusip = model.Cusip;
            existing.Currency = model.Currency;
            existing.SecurityType = model.SecurityType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Security?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Security>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToPositions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPositions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToTrades(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTrades(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
