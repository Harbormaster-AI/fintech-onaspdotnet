
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class LoanTransactionRepository : ILoanTransactionRepository
{
    private readonly ApplicationDbContext _db;

    public LoanTransactionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<LoanTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.LoanTransactions
            .Include(x => x.Loan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LoanTransaction>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.LoanTransactions
            .AsNoTracking()
            .Include(x => x.Loan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LoanTransaction loanTransaction, CancellationToken cancellationToken)
    {
        _db.LoanTransactions.Add(loanTransaction);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LoanTransaction loanTransaction, CancellationToken cancellationToken)
    {
        _db.LoanTransactions.Update(loanTransaction);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LoanTransaction loanTransaction, CancellationToken cancellationToken)
    {
        _db.LoanTransactions.Remove(loanTransaction);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
