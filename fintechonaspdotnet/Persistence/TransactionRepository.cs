
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _db;

    public TransactionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Transactions
            .Include(x => x.Account)
            .Include(x => x.Wallet)
            .Include(x => x.PaymentOrder)
            .Include(x => x.Merchant)
            .Include(x => x.Card)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Transaction>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Transactions
            .AsNoTracking()
            .Include(x => x.Account)
            .Include(x => x.Wallet)
            .Include(x => x.PaymentOrder)
            .Include(x => x.Merchant)
            .Include(x => x.Card)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        _db.Transactions.Add(transaction);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        _db.Transactions.Update(transaction);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        _db.Transactions.Remove(transaction);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToRelatedTransactionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Transactions
            .Where(transaction =>
                request.ChildIds.Contains(transaction.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    transaction =>
                        EF.Property<Guid?>(
                            transaction,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRelatedTransactionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Transactions
            .Where(transaction =>
                request.ChildIds.Contains(transaction.Id) &&
                EF.Property<Guid?>(
                    transaction,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    transaction =>
                        EF.Property<Guid?>(
                            transaction,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToAlertsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ComplianceAlerts
            .Where(complianceAlert =>
                request.ChildIds.Contains(complianceAlert.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    complianceAlert =>
                        EF.Property<Guid?>(
                            complianceAlert,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAlertsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ComplianceAlerts
            .Where(complianceAlert =>
                request.ChildIds.Contains(complianceAlert.Id) &&
                EF.Property<Guid?>(
                    complianceAlert,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    complianceAlert =>
                        EF.Property<Guid?>(
                            complianceAlert,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
