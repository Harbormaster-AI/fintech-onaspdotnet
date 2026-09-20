using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IRepaymentScheduleRepository
{
    Task<RepaymentSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<RepaymentSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(RepaymentSchedule repaymentSchedule, CancellationToken cancellationToken);
    Task UpdateAsync(RepaymentSchedule repaymentSchedule, CancellationToken cancellationToken);
    Task DeleteAsync(RepaymentSchedule repaymentSchedule, CancellationToken cancellationToken);
}
