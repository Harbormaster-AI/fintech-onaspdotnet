using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class InvestmentAccountRepository : IInvestmentAccountRepository
{
    private readonly ApplicationDbContext _db;

    public InvestmentAccountRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InvestmentAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InvestmentAccounts
            .Include(x => x.Portfolio)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InvestmentAccount>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InvestmentAccounts
            .AsNoTracking()
            .Include(x => x.Portfolio)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken)
    {
        _db.InvestmentAccounts.Add(investmentAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken)
    {
        _db.InvestmentAccounts.Update(investmentAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InvestmentAccount investmentAccount, CancellationToken cancellationToken)
    {
        _db.InvestmentAccounts.Remove(investmentAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
