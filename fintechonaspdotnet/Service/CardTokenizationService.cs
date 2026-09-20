using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface ICardTokenizationService {

    Task Create(CardTokenization model , CancellationToken cancellationToken);
    Task<bool> Update(CardTokenization model, CancellationToken cancellationToken);
    Task<CardTokenization?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CardTokenization>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCard(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCard(AssociationRequest request, CancellationToken cancellationToken);


}

public class CardTokenizationService : ICardTokenizationService
{
    private readonly ICardTokenizationRepository _repository;
    private readonly ILogger<CardTokenizationService> _logger;

    public CardTokenizationService(
        ICardTokenizationRepository repository, ILogger<CardTokenizationService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CardTokenization model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(CardTokenization model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.TokenReference = model.TokenReference;
            existing.CreatedAt = model.CreatedAt;
            existing.WalletProvider = model.WalletProvider;
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

    public Task<CardTokenization?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CardTokenization>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCard(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCard(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
