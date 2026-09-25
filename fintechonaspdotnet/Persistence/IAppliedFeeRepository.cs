using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IAppliedFeeRepository
{
    Task<AppliedFee?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AppliedFee>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AppliedFee appliedFee, CancellationToken cancellationToken);
    Task UpdateAsync(AppliedFee appliedFee, CancellationToken cancellationToken);
    Task DeleteAsync(AppliedFee appliedFee, CancellationToken cancellationToken);


}
