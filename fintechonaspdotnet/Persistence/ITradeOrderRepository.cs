using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ITradeOrderRepository
{
    Task<TradeOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TradeOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TradeOrder tradeOrder, CancellationToken cancellationToken);
    Task UpdateAsync(TradeOrder tradeOrder, CancellationToken cancellationToken);
    Task DeleteAsync(TradeOrder tradeOrder, CancellationToken cancellationToken);

    Task AddToTradesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTradesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
