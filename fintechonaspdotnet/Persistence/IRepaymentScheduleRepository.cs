using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IRepaymentScheduleRepository
{
    Task<RepaymentSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<RepaymentSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(RepaymentSchedule repaymentSchedule, CancellationToken cancellationToken);
    Task UpdateAsync(RepaymentSchedule repaymentSchedule, CancellationToken cancellationToken);
    Task DeleteAsync(RepaymentSchedule repaymentSchedule, CancellationToken cancellationToken);

    Task AddToPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
