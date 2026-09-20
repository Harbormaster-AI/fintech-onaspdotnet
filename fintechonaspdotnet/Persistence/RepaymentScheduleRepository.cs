using fintechonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class RepaymentScheduleRepository : IRepaymentScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public RepaymentScheduleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<RepaymentSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.RepaymentSchedules
            .Include(x => x.Loan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<RepaymentSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.RepaymentSchedules
            .AsNoTracking()
            .Include(x => x.Loan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RepaymentSchedule repaymentSchedule, CancellationToken cancellationToken)
    {
        _db.RepaymentSchedules.Add(repaymentSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RepaymentSchedule repaymentSchedule, CancellationToken cancellationToken)
    {
        _db.RepaymentSchedules.Update(repaymentSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RepaymentSchedule repaymentSchedule, CancellationToken cancellationToken)
    {
        _db.RepaymentSchedules.Remove(repaymentSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
