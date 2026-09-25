
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class KYCDocumentRepository : IKYCDocumentRepository
{
    private readonly ApplicationDbContext _db;

    public KYCDocumentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<KYCDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.KYCDocuments
            .Include(x => x.KycProfile)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<KYCDocument>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.KYCDocuments
            .AsNoTracking()
            .Include(x => x.KycProfile)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(KYCDocument kYCDocument, CancellationToken cancellationToken)
    {
        _db.KYCDocuments.Add(kYCDocument);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(KYCDocument kYCDocument, CancellationToken cancellationToken)
    {
        _db.KYCDocuments.Update(kYCDocument);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(KYCDocument kYCDocument, CancellationToken cancellationToken)
    {
        _db.KYCDocuments.Remove(kYCDocument);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
