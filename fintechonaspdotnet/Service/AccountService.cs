using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IAccountService {

    Task Create(Account model , CancellationToken cancellationToken);
    Task<bool> Update(Account model, CancellationToken cancellationToken);
    Task<Account?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Account>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignInstitution(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInstitution(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToStatements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromStatements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMandates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMandates(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;
    private readonly ILogger<AccountService> _logger;

    public AccountService(
        IAccountRepository repository, ILogger<AccountService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Account model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Account model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.AccountNumber = model.AccountNumber;
            existing.Iban = model.Iban;
            existing.Bic = model.Bic;
            existing.OpenedDate = model.OpenedDate;
            existing.Currency = model.Currency;
            existing.Balance = model.Balance;
            existing.AvailableBalance = model.AvailableBalance;
            existing.AccountType = model.AccountType;
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

    public Task<Account?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Account>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignInstitution(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInstitution(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTransactions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToStatements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromStatements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToMandates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromMandates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
