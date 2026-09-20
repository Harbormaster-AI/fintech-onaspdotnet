using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IKYCDocumentService {

    Task Create(KYCDocument model , CancellationToken cancellationToken);
    Task<bool> Update(KYCDocument model, CancellationToken cancellationToken);
    Task<KYCDocument?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<KYCDocument>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignKycProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignKycProfile(AssociationRequest request, CancellationToken cancellationToken);


}

public class KYCDocumentService : IKYCDocumentService
{
    private readonly IKYCDocumentRepository _repository;
    private readonly ILogger<KYCDocumentService> _logger;

    public KYCDocumentService(
        IKYCDocumentRepository repository, ILogger<KYCDocumentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(KYCDocument model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(KYCDocument model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Reference = model.Reference;
            existing.IssuedCountry = model.IssuedCountry;
            existing.ExpirationDate = model.ExpirationDate;
            existing.DocumentType = model.DocumentType;
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

    public Task<KYCDocument?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<KYCDocument>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignKycProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignKycProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
