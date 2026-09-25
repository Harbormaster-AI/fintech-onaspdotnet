using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IPositionRepository
{
    Task<Position?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Position>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Position position, CancellationToken cancellationToken);
    Task UpdateAsync(Position position, CancellationToken cancellationToken);
    Task DeleteAsync(Position position, CancellationToken cancellationToken);


}
