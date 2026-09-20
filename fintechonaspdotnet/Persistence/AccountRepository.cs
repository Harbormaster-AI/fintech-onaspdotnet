using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _db;

    public AccountRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Accounts
            .Include(x => x.Customer)
            .Include(x => x.Institution)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Account>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Accounts
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Institution)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Account account, CancellationToken cancellationToken)
    {
        _db.Accounts.Add(account);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Account account, CancellationToken cancellationToken)
    {
        _db.Accounts.Update(account);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Account account, CancellationToken cancellationToken)
    {
        _db.Accounts.Remove(account);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
