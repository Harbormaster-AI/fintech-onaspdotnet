
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class MerchantRepository : IMerchantRepository
{
    private readonly ApplicationDbContext _db;

    public MerchantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Merchant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Merchants
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Merchant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Merchants
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Merchant merchant, CancellationToken cancellationToken)
    {
        _db.Merchants.Add(merchant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Merchant merchant, CancellationToken cancellationToken)
    {
        _db.Merchants.Update(merchant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Merchant merchant, CancellationToken cancellationToken)
    {
        _db.Merchants.Remove(merchant);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToTerminalsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Terminals
            .Where(terminal =>
                request.ChildIds.Contains(terminal.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    terminal =>
                        EF.Property<Guid?>(
                            terminal,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTerminalsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Terminals
            .Where(terminal =>
                request.ChildIds.Contains(terminal.Id) &&
                EF.Property<Guid?>(
                    terminal,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    terminal =>
                        EF.Property<Guid?>(
                            terminal,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToPaymentContractsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentContracts
            .Where(paymentContract =>
                request.ChildIds.Contains(paymentContract.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentContract =>
                        EF.Property<Guid?>(
                            paymentContract,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPaymentContractsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentContracts
            .Where(paymentContract =>
                request.ChildIds.Contains(paymentContract.Id) &&
                EF.Property<Guid?>(
                    paymentContract,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentContract =>
                        EF.Property<Guid?>(
                            paymentContract,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToPayoutsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Payouts
            .Where(payout =>
                request.ChildIds.Contains(payout.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payout =>
                        EF.Property<Guid?>(
                            payout,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPayoutsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Payouts
            .Where(payout =>
                request.ChildIds.Contains(payout.Id) &&
                EF.Property<Guid?>(
                    payout,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payout =>
                        EF.Property<Guid?>(
                            payout,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToSettlementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SettlementBatchs
            .Where(settlementBatch =>
                request.ChildIds.Contains(settlementBatch.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    settlementBatch =>
                        EF.Property<Guid?>(
                            settlementBatch,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSettlementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SettlementBatchs
            .Where(settlementBatch =>
                request.ChildIds.Contains(settlementBatch.Id) &&
                EF.Property<Guid?>(
                    settlementBatch,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    settlementBatch =>
                        EF.Property<Guid?>(
                            settlementBatch,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToDisputesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Disputes
            .Where(dispute =>
                request.ChildIds.Contains(dispute.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dispute =>
                        EF.Property<Guid?>(
                            dispute,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDisputesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Disputes
            .Where(dispute =>
                request.ChildIds.Contains(dispute.Id) &&
                EF.Property<Guid?>(
                    dispute,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dispute =>
                        EF.Property<Guid?>(
                            dispute,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToInvoicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Invoices
            .Where(invoice =>
                request.ChildIds.Contains(invoice.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    invoice =>
                        EF.Property<Guid?>(
                            invoice,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInvoicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Invoices
            .Where(invoice =>
                request.ChildIds.Contains(invoice.Id) &&
                EF.Property<Guid?>(
                    invoice,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    invoice =>
                        EF.Property<Guid?>(
                            invoice,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
