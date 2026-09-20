using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface ILoanApplicationService {

    Task Create(LoanApplication model , CancellationToken cancellationToken);
    Task<bool> Update(LoanApplication model, CancellationToken cancellationToken);
    Task<LoanApplication?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<LoanApplication>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRiskAssessment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRiskAssessment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLoan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLoan(AssociationRequest request, CancellationToken cancellationToken);


}

public class LoanApplicationService : ILoanApplicationService
{
    private readonly ILoanApplicationRepository _repository;
    private readonly ILogger<LoanApplicationService> _logger;

    public LoanApplicationService(
        ILoanApplicationRepository repository, ILogger<LoanApplicationService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(LoanApplication model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(LoanApplication model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ApplicationNumber = model.ApplicationNumber;
            existing.AmountRequested = model.AmountRequested;
            existing.TermMonths = model.TermMonths;
            existing.SubmittedAt = model.SubmittedAt;
            existing.Product = model.Product;
            existing.Purpose = model.Purpose;
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

    public Task<LoanApplication?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<LoanApplication>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRiskAssessment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRiskAssessment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignLoan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLoan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
