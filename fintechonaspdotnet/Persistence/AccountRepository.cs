
using fintechonaspdotnet.Contracts;
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


    public async Task AddToTransactionsAsync(
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

    public async Task RemoveFromTransactionsAsync(
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


    public async Task AddToCardsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentCards
            .Where(paymentCard =>
                request.ChildIds.Contains(paymentCard.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentCard =>
                        EF.Property<Guid?>(
                            paymentCard,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCardsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentCards
            .Where(paymentCard =>
                request.ChildIds.Contains(paymentCard.Id) &&
                EF.Property<Guid?>(
                    paymentCard,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentCard =>
                        EF.Property<Guid?>(
                            paymentCard,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToStatementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AccountStatements
            .Where(accountStatement =>
                request.ChildIds.Contains(accountStatement.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    accountStatement =>
                        EF.Property<Guid?>(
                            accountStatement,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromStatementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AccountStatements
            .Where(accountStatement =>
                request.ChildIds.Contains(accountStatement.Id) &&
                EF.Property<Guid?>(
                    accountStatement,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    accountStatement =>
                        EF.Property<Guid?>(
                            accountStatement,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToMandatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DirectDebitMandates
            .Where(directDebitMandate =>
                request.ChildIds.Contains(directDebitMandate.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    directDebitMandate =>
                        EF.Property<Guid?>(
                            directDebitMandate,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromMandatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DirectDebitMandates
            .Where(directDebitMandate =>
                request.ChildIds.Contains(directDebitMandate.Id) &&
                EF.Property<Guid?>(
                    directDebitMandate,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    directDebitMandate =>
                        EF.Property<Guid?>(
                            directDebitMandate,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
