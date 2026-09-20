using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IAccountStatementService {

    Task Create(AccountStatement model , CancellationToken cancellationToken);
    Task<bool> Update(AccountStatement model, CancellationToken cancellationToken);
    Task<AccountStatement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AccountStatement>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAccount(AssociationRequest request, CancellationToken cancellationToken);


}

public class AccountStatementService : IAccountStatementService
{
    private readonly IAccountStatementRepository _repository;
    private readonly ILogger<AccountStatementService> _logger;

    public AccountStatementService(
        IAccountStatementRepository repository, ILogger<AccountStatementService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AccountStatement model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(AccountStatement model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.StatementNumber = model.StatementNumber;
            existing.PeriodStart = model.PeriodStart;
            existing.PeriodEnd = model.PeriodEnd;
            existing.OpeningBalance = model.OpeningBalance;
            existing.ClosingBalance = model.ClosingBalance;
            existing.GeneratedAt = model.GeneratedAt;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<AccountStatement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AccountStatement>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
