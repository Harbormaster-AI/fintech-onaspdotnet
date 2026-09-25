using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ITerminalRepository
{
    Task<Terminal?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Terminal>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Terminal terminal, CancellationToken cancellationToken);
    Task UpdateAsync(Terminal terminal, CancellationToken cancellationToken);
    Task DeleteAsync(Terminal terminal, CancellationToken cancellationToken);


}
