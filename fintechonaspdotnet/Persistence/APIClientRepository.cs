using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class APIClientRepository : IAPIClientRepository
{
    private readonly ApplicationDbContext _db;

    public APIClientRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<APIClient?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.APIClients
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<APIClient>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.APIClients
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(APIClient aPIClient, CancellationToken cancellationToken)
    {
        _db.APIClients.Add(aPIClient);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(APIClient aPIClient, CancellationToken cancellationToken)
    {
        _db.APIClients.Update(aPIClient);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(APIClient aPIClient, CancellationToken cancellationToken)
    {
        _db.APIClients.Remove(aPIClient);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
