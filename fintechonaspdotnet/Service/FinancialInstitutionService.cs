using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IFinancialInstitutionService {

    Task Create(FinancialInstitution model , CancellationToken cancellationToken);
    Task<bool> Update(FinancialInstitution model, CancellationToken cancellationToken);
    Task<FinancialInstitution?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<FinancialInstitution>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToBranches(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBranches(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToProductOfferings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProductOfferings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPaymentProcessors(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPaymentProcessors(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCompliancePolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCompliancePolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class FinancialInstitutionService : IFinancialInstitutionService
{
    private readonly IFinancialInstitutionRepository _repository;
    private readonly ILogger<FinancialInstitutionService> _logger;

    public FinancialInstitutionService(
        IFinancialInstitutionRepository repository, ILogger<FinancialInstitutionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(FinancialInstitution model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(FinancialInstitution model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LegalName = model.LegalName;
            existing.CountryOfIncorporation = model.CountryOfIncorporation;
            existing.Bic = model.Bic;
            existing.Website = model.Website;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<FinancialInstitution?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<FinancialInstitution>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToBranches(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromBranches(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToProductOfferings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromProductOfferings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPaymentProcessors(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPaymentProcessors(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCompliancePolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCompliancePolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
