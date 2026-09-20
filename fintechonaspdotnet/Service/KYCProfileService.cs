using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IKYCProfileService {

    Task Create(KYCProfile model , CancellationToken cancellationToken);
    Task<bool> Update(KYCProfile model, CancellationToken cancellationToken);
    Task<KYCProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<KYCProfile>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAddresses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAddresses(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class KYCProfileService : IKYCProfileService
{
    private readonly IKYCProfileRepository _repository;
    private readonly ILogger<KYCProfileService> _logger;

    public KYCProfileService(
        IKYCProfileRepository repository, ILogger<KYCProfileService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(KYCProfile model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(KYCProfile model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ProfileId = model.ProfileId;
            existing.CreatedAt = model.CreatedAt;
            existing.Status = model.Status;
            existing.VerificationLevel = model.VerificationLevel;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<KYCProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<KYCProfile>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAddresses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAddresses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
