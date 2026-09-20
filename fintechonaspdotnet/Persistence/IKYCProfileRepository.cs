using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IKYCProfileRepository
{
    Task<KYCProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<KYCProfile>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(KYCProfile kYCProfile, CancellationToken cancellationToken);
    Task UpdateAsync(KYCProfile kYCProfile, CancellationToken cancellationToken);
    Task DeleteAsync(KYCProfile kYCProfile, CancellationToken cancellationToken);
}
