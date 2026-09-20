using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IExchangeRateService {

    Task Create(ExchangeRate model , CancellationToken cancellationToken);
    Task<bool> Update(ExchangeRate model, CancellationToken cancellationToken);
    Task<ExchangeRate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ExchangeRate>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToUsedByQuotes(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromUsedByQuotes(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ExchangeRateService : IExchangeRateService
{
    private readonly IExchangeRateRepository _repository;
    private readonly ILogger<ExchangeRateService> _logger;

    public ExchangeRateService(
        IExchangeRateRepository repository, ILogger<ExchangeRateService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ExchangeRate model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ExchangeRate model, CancellationToken cancellationToken)
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
            existing.AsOf = model.AsOf;
            existing.Source = model.Source;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ExchangeRate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ExchangeRate>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToUsedByQuotes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromUsedByQuotes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
