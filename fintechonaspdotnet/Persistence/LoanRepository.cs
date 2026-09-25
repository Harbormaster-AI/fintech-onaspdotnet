
using fintechonaspdotnet.Contracts;
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


    public async Task AddToScheduleAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RepaymentSchedules
            .Where(repaymentSchedule =>
                request.ChildIds.Contains(repaymentSchedule.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    repaymentSchedule =>
                        EF.Property<Guid?>(
                            repaymentSchedule,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromScheduleAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RepaymentSchedules
            .Where(repaymentSchedule =>
                request.ChildIds.Contains(repaymentSchedule.Id) &&
                EF.Property<Guid?>(
                    repaymentSchedule,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    repaymentSchedule =>
                        EF.Property<Guid?>(
                            repaymentSchedule,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToCollateralAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Collaterals
            .Where(collateral =>
                request.ChildIds.Contains(collateral.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    collateral =>
                        EF.Property<Guid?>(
                            collateral,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCollateralAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Collaterals
            .Where(collateral =>
                request.ChildIds.Contains(collateral.Id) &&
                EF.Property<Guid?>(
                    collateral,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    collateral =>
                        EF.Property<Guid?>(
                            collateral,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToTransactionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LoanTransactions
            .Where(loanTransaction =>
                request.ChildIds.Contains(loanTransaction.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    loanTransaction =>
                        EF.Property<Guid?>(
                            loanTransaction,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTransactionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LoanTransactions
            .Where(loanTransaction =>
                request.ChildIds.Contains(loanTransaction.Id) &&
                EF.Property<Guid?>(
                    loanTransaction,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    loanTransaction =>
                        EF.Property<Guid?>(
                            loanTransaction,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
