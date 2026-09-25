
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly ApplicationDbContext _db;

    public InvoiceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Invoices
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Invoices
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        _db.Invoices.Add(invoice);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        _db.Invoices.Update(invoice);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        _db.Invoices.Remove(invoice);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPaymentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentOrders
            .Where(paymentOrder =>
                request.ChildIds.Contains(paymentOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentOrder =>
                        EF.Property<Guid?>(
                            paymentOrder,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPaymentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentOrders
            .Where(paymentOrder =>
                request.ChildIds.Contains(paymentOrder.Id) &&
                EF.Property<Guid?>(
                    paymentOrder,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentOrder =>
                        EF.Property<Guid?>(
                            paymentOrder,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
