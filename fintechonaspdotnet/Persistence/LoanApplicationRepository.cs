
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class LoanApplicationRepository : ILoanApplicationRepository
{
    private readonly ApplicationDbContext _db;

    public LoanApplicationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<LoanApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.LoanApplications
            .Include(x => x.Customer)
            .Include(x => x.RiskAssessment)
            .Include(x => x.Loan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LoanApplication>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.LoanApplications
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.RiskAssessment)
            .Include(x => x.Loan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LoanApplication loanApplication, CancellationToken cancellationToken)
    {
        _db.LoanApplications.Add(loanApplication);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LoanApplication loanApplication, CancellationToken cancellationToken)
    {
        _db.LoanApplications.Update(loanApplication);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LoanApplication loanApplication, CancellationToken cancellationToken)
    {
        _db.LoanApplications.Remove(loanApplication);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
