using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IRepaymentScheduleService {

    Task Create(RepaymentSchedule model , CancellationToken cancellationToken);
    Task<bool> Update(RepaymentSchedule model, CancellationToken cancellationToken);
    Task<RepaymentSchedule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<RepaymentSchedule>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignLoan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLoan(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPayments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPayments(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class RepaymentScheduleService : IRepaymentScheduleService
{
    private readonly IRepaymentScheduleRepository _repository;
    private readonly ILogger<RepaymentScheduleService> _logger;

    public RepaymentScheduleService(
        IRepaymentScheduleRepository repository, ILogger<RepaymentScheduleService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(RepaymentSchedule model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(RepaymentSchedule model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.InstallmentNumber = model.InstallmentNumber;
            existing.DueDate = model.DueDate;
            existing.AmountDue = model.AmountDue;
            existing.PrincipalDue = model.PrincipalDue;
            existing.InterestDue = model.InterestDue;
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

    public Task<RepaymentSchedule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<RepaymentSchedule>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToPayments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPayments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
