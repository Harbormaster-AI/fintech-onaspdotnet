using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IKYCProfileRepository
{
    Task<KYCProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<KYCProfile>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(KYCProfile kYCProfile, CancellationToken cancellationToken);
    Task UpdateAsync(KYCProfile kYCProfile, CancellationToken cancellationToken);
    Task DeleteAsync(KYCProfile kYCProfile, CancellationToken cancellationToken);

    Task AddToDocumentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDocumentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToScreeningsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromScreeningsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAddressesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAddressesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
