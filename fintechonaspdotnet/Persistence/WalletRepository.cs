
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class WalletRepository : IWalletRepository
{
    private readonly ApplicationDbContext _db;

    public WalletRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Wallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Wallets
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Wallet>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Wallets
            .AsNoTracking()
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Wallet wallet, CancellationToken cancellationToken)
    {
        _db.Wallets.Add(wallet);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Wallet wallet, CancellationToken cancellationToken)
    {
        _db.Wallets.Update(wallet);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Wallet wallet, CancellationToken cancellationToken)
    {
        _db.Wallets.Remove(wallet);
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

}
