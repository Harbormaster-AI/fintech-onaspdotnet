using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IFeeScheduleRepository
{
    Task<FeeSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FeeSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(FeeSchedule feeSchedule, CancellationToken cancellationToken);
    Task UpdateAsync(FeeSchedule feeSchedule, CancellationToken cancellationToken);
    Task DeleteAsync(FeeSchedule feeSchedule, CancellationToken cancellationToken);


}
