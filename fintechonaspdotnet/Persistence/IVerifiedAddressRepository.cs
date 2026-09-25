using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IVerifiedAddressRepository
{
    Task<VerifiedAddress?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<VerifiedAddress>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(VerifiedAddress verifiedAddress, CancellationToken cancellationToken);
    Task UpdateAsync(VerifiedAddress verifiedAddress, CancellationToken cancellationToken);
    Task DeleteAsync(VerifiedAddress verifiedAddress, CancellationToken cancellationToken);


}
