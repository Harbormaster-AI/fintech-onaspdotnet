using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IAPIClientRepository
{
    Task<APIClient?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<APIClient>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(APIClient aPIClient, CancellationToken cancellationToken);
    Task UpdateAsync(APIClient aPIClient, CancellationToken cancellationToken);
    Task DeleteAsync(APIClient aPIClient, CancellationToken cancellationToken);
}
