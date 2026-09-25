using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ICollateralRepository
{
    Task<Collateral?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Collateral>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Collateral collateral, CancellationToken cancellationToken);
    Task UpdateAsync(Collateral collateral, CancellationToken cancellationToken);
    Task DeleteAsync(Collateral collateral, CancellationToken cancellationToken);


}
