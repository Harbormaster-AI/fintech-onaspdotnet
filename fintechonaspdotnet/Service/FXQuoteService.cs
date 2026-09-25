
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IFXQuoteRepository _repository;
    private readonly ILogger<FXQuoteService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public FXQuoteService(
        ApplicationTelemetry telemetry,
        IFXQuoteRepository repository,
        ILogger<FXQuoteService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(FXQuote model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "FXQuote",
                "CreateFXQuote",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "FXQuote",
                "UpdateFXQuote",
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
            await _telemetry.Execute(
                "FXQuote",
                "UpdateFXQuote",
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

    public async Task<bool> AssignRequestedBy(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No FXQuote found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<CustomerService>().Get(childRequest, cancellationToken);
            parent.RequestedBy = child;
            await Update( parent, cancellationToken );
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

    public async Task<bool> UnassignRequestedBy(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No FXQuote found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.RequestedBy = null;
            await Update( parent, cancellationToken );
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
