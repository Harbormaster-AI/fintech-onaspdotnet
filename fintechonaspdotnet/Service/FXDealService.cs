using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IFXDealService {

    Task Create(FXDeal model , CancellationToken cancellationToken);
    Task<bool> Update(FXDeal model, CancellationToken cancellationToken);
    Task<FXDeal?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<FXDeal>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignQuote(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignQuote(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPaymentOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPaymentOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class FXDealService : IFXDealService
{
    private readonly IFXDealRepository _repository;
    private readonly ILogger<FXDealService> _logger;

    public FXDealService(
        IFXDealRepository repository, ILogger<FXDealService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(FXDeal model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(FXDeal model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.DealReference = model.DealReference;
            existing.BaseCurrency = model.BaseCurrency;
            existing.QuoteCurrency = model.QuoteCurrency;
            existing.Rate = model.Rate;
            existing.Amount = model.Amount;
            existing.SettlementDate = model.SettlementDate;
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

    public Task<FXDeal?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<FXDeal>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignQuote(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignQuote(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToPaymentOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPaymentOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
