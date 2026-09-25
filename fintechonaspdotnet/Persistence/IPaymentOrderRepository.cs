using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IPaymentOrderRepository
{
    Task<PaymentOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PaymentOrder paymentOrder, CancellationToken cancellationToken);
    Task UpdateAsync(PaymentOrder paymentOrder, CancellationToken cancellationToken);
    Task DeleteAsync(PaymentOrder paymentOrder, CancellationToken cancellationToken);

    Task AddToTransactionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTransactionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToFeesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFeesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
