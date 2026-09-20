using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IKYCDocumentRepository
{
    Task<KYCDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<KYCDocument>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(KYCDocument kYCDocument, CancellationToken cancellationToken);
    Task UpdateAsync(KYCDocument kYCDocument, CancellationToken cancellationToken);
    Task DeleteAsync(KYCDocument kYCDocument, CancellationToken cancellationToken);
}
