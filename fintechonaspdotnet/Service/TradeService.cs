using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface ITradeService {

    Task Create(Trade model , CancellationToken cancellationToken);
    Task<bool> Update(Trade model, CancellationToken cancellationToken);
    Task<Trade?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Trade>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSecurity(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSecurity(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignInvestmentAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInvestmentAccount(AssociationRequest request, CancellationToken cancellationToken);


}

public class TradeService : ITradeService
{
    private readonly ITradeRepository _repository;
    private readonly ILogger<TradeService> _logger;

    public TradeService(
        ITradeRepository repository, ILogger<TradeService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Trade model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Trade model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ExecutedAt = model.ExecutedAt;
            existing.Quantity = model.Quantity;
            existing.Price = model.Price;
            existing.Fees = model.Fees;
            existing.SettlementDate = model.SettlementDate;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Trade?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Trade>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignSecurity(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSecurity(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignInvestmentAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInvestmentAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
