using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface ILoanTransactionService {

    Task Create(LoanTransaction model , CancellationToken cancellationToken);
    Task<bool> Update(LoanTransaction model, CancellationToken cancellationToken);
    Task<LoanTransaction?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<LoanTransaction>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignLoan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLoan(AssociationRequest request, CancellationToken cancellationToken);


}

public class LoanTransactionService : ILoanTransactionService
{
    private readonly ILoanTransactionRepository _repository;
    private readonly ILogger<LoanTransactionService> _logger;

    public LoanTransactionService(
        ILoanTransactionRepository repository, ILogger<LoanTransactionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(LoanTransaction model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(LoanTransaction model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.TransactionId = model.TransactionId;
            existing.Amount = model.Amount;
            existing.PostingDate = model.PostingDate;
            existing.Type = model.Type;
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

    public Task<LoanTransaction?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<LoanTransaction>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignLoan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLoan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
