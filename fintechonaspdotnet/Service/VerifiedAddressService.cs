using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IVerifiedAddressService {

    Task Create(VerifiedAddress model , CancellationToken cancellationToken);
    Task<bool> Update(VerifiedAddress model, CancellationToken cancellationToken);
    Task<VerifiedAddress?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<VerifiedAddress>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignKycProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignKycProfile(AssociationRequest request, CancellationToken cancellationToken);


}

public class VerifiedAddressService : IVerifiedAddressService
{
    private readonly IVerifiedAddressRepository _repository;
    private readonly ILogger<VerifiedAddressService> _logger;

    public VerifiedAddressService(
        IVerifiedAddressRepository repository, ILogger<VerifiedAddressService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(VerifiedAddress model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(VerifiedAddress model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Address = model.Address;
            existing.VerifiedAt = model.VerifiedAt;
            existing.VerificationStatus = model.VerificationStatus;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<VerifiedAddress?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<VerifiedAddress>> GetAll(CancellationToken cancellationToken)
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
