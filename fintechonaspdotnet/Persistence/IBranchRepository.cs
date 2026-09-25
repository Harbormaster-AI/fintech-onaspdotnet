using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IBranchRepository
{
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Branch>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Branch branch, CancellationToken cancellationToken);
    Task UpdateAsync(Branch branch, CancellationToken cancellationToken);
    Task DeleteAsync(Branch branch, CancellationToken cancellationToken);


}
