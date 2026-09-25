
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class AccountStatementRepository : IAccountStatementRepository
{
    private readonly ApplicationDbContext _db;

    public AccountStatementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AccountStatement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AccountStatements
            .Include(x => x.Account)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AccountStatement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AccountStatements
            .AsNoTracking()
            .Include(x => x.Account)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AccountStatement accountStatement, CancellationToken cancellationToken)
    {
        _db.AccountStatements.Add(accountStatement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AccountStatement accountStatement, CancellationToken cancellationToken)
    {
        _db.AccountStatements.Update(accountStatement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AccountStatement accountStatement, CancellationToken cancellationToken)
    {
        _db.AccountStatements.Remove(accountStatement);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
