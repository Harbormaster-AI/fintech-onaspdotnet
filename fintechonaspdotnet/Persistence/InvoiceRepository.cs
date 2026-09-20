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
}
