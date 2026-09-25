
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IFXDealRepository _repository;
    private readonly ILogger<FXDealService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public FXDealService(
        ApplicationTelemetry telemetry,
        IFXDealRepository repository,
        ILogger<FXDealService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(FXDeal model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "FXDeal",
                "CreateFXDeal",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "FXDeal",
                "UpdateFXDeal",
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
            await _telemetry.Execute(
                "FXDeal",
                "UpdateFXDeal",
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

    public async Task<bool> AssignQuote(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No FXDeal found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<FXQuoteService>().Get(childRequest, cancellationToken);
            parent.Quote = child;
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

    public async Task<bool> UnassignQuote(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No FXDeal found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Quote = null;
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


    public async Task<bool> AddToPaymentOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FXDeal",
                "AddToPaymentOrders",
                () => _repository.AddToPaymentOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPaymentOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FXDeal",
                "RemoveFromPaymentOrders",
                () => _repository.RemoveFromPaymentOrdersAsync(request, cancellationToken));
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
