using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface ICustomerService {

    Task Create(Customer model , CancellationToken cancellationToken);
    Task<bool> Update(Customer model, CancellationToken cancellationToken);
    Task<Customer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Customer>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignInstitution(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInstitution(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWallets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWallets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToKycProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromKycProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToConsents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromConsents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLoanApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLoanApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLoans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLoans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPortfolios(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPortfolios(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(
        ICustomerRepository repository, ILogger<CustomerService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Customer model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Customer model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.DateOfBirth = model.DateOfBirth;
            existing.Email = model.Email;
            existing.Phone = model.Phone;
            existing.Address = model.Address;
            existing.TaxId = model.TaxId;
            existing.RiskScore = model.RiskScore;
            existing.CustomerType = model.CustomerType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Customer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Customer>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignInstitution(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInstitution(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToWallets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromWallets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToKycProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromKycProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToConsents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromConsents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLoanApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLoanApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLoans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLoans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPortfolios(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPortfolios(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDisputes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
