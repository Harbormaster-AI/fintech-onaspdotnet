
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class KYCProfileRepository : IKYCProfileRepository
{
    private readonly ApplicationDbContext _db;

    public KYCProfileRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<KYCProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.KYCProfiles
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<KYCProfile>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.KYCProfiles
            .AsNoTracking()
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(KYCProfile kYCProfile, CancellationToken cancellationToken)
    {
        _db.KYCProfiles.Add(kYCProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(KYCProfile kYCProfile, CancellationToken cancellationToken)
    {
        _db.KYCProfiles.Update(kYCProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(KYCProfile kYCProfile, CancellationToken cancellationToken)
    {
        _db.KYCProfiles.Remove(kYCProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDocumentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.KYCDocuments
            .Where(kYCDocument =>
                request.ChildIds.Contains(kYCDocument.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    kYCDocument =>
                        EF.Property<Guid?>(
                            kYCDocument,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDocumentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.KYCDocuments
            .Where(kYCDocument =>
                request.ChildIds.Contains(kYCDocument.Id) &&
                EF.Property<Guid?>(
                    kYCDocument,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    kYCDocument =>
                        EF.Property<Guid?>(
                            kYCDocument,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToScreeningsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Screenings
            .Where(screening =>
                request.ChildIds.Contains(screening.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    screening =>
                        EF.Property<Guid?>(
                            screening,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromScreeningsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Screenings
            .Where(screening =>
                request.ChildIds.Contains(screening.Id) &&
                EF.Property<Guid?>(
                    screening,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    screening =>
                        EF.Property<Guid?>(
                            screening,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToAddressesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.VerifiedAddresss
            .Where(verifiedAddress =>
                request.ChildIds.Contains(verifiedAddress.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    verifiedAddress =>
                        EF.Property<Guid?>(
                            verifiedAddress,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAddressesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.VerifiedAddresss
            .Where(verifiedAddress =>
                request.ChildIds.Contains(verifiedAddress.Id) &&
                EF.Property<Guid?>(
                    verifiedAddress,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    verifiedAddress =>
                        EF.Property<Guid?>(
                            verifiedAddress,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
