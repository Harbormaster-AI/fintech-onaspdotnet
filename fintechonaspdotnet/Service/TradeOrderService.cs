using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface ITradeOrderService {

    Task Create(TradeOrder model , CancellationToken cancellationToken);
    Task<bool> Update(TradeOrder model, CancellationToken cancellationToken);
    Task<TradeOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TradeOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPortfolio(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPortfolio(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSecurity(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSecurity(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToTrades(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTrades(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class TradeOrderService : ITradeOrderService
{
    private readonly ITradeOrderRepository _repository;
    private readonly ILogger<TradeOrderService> _logger;

    public TradeOrderService(
        ITradeOrderRepository repository, ILogger<TradeOrderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(TradeOrder model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(TradeOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.OrderId = model.OrderId;
            existing.Quantity = model.Quantity;
            existing.LimitPrice = model.LimitPrice;
            existing.PlacedAt = model.PlacedAt;
            existing.Side = model.Side;
            existing.Type = model.Type;
            existing.Status = model.Status;
            existing.TimeInForce = model.TimeInForce;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<TradeOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TradeOrder>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPortfolio(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPortfolio(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignSecurity(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSecurity(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToTrades(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTrades(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
