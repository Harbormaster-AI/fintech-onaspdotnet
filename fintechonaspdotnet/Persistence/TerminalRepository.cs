
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class TerminalRepository : ITerminalRepository
{
    private readonly ApplicationDbContext _db;

    public TerminalRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Terminal?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Terminals
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Terminal>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Terminals
            .AsNoTracking()
            .Include(x => x.Merchant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Terminal terminal, CancellationToken cancellationToken)
    {
        _db.Terminals.Add(terminal);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Terminal terminal, CancellationToken cancellationToken)
    {
        _db.Terminals.Update(terminal);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Terminal terminal, CancellationToken cancellationToken)
    {
        _db.Terminals.Remove(terminal);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
