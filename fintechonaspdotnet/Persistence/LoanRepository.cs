using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext _db;

    public LoanRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Loans
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Loans
            .AsNoTracking()
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Loan loan, CancellationToken cancellationToken)
    {
        _db.Loans.Add(loan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Loan loan, CancellationToken cancellationToken)
    {
        _db.Loans.Update(loan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Loan loan, CancellationToken cancellationToken)
    {
        _db.Loans.Remove(loan);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
