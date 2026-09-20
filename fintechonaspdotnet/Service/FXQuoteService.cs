using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IFXQuoteService {

    Task Create(FXQuote model , CancellationToken cancellationToken);
    Task<bool> Update(FXQuote model, CancellationToken cancellationToken);
    Task<FXQuote?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<FXQuote>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRequestedBy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRequestedBy(AssociationRequest request, CancellationToken cancellationToken);


}

public class FXQuoteService : IFXQuoteService
{
    private readonly IFXQuoteRepository _repository;
    private readonly ILogger<FXQuoteService> _logger;

    public FXQuoteService(
        IFXQuoteRepository repository, ILogger<FXQuoteService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(FXQuote model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(FXQuote model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.BaseCurrency = model.BaseCurrency;
            existing.QuoteCurrency = model.QuoteCurrency;
            existing.Rate = model.Rate;
            existing.QuotedAt = model.QuotedAt;
            existing.ExpiresAt = model.ExpiresAt;
            existing.PriceType = model.PriceType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<FXQuote?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<FXQuote>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRequestedBy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRequestedBy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
