using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IPaymentCardRepository
{
    Task<PaymentCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentCard>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PaymentCard paymentCard, CancellationToken cancellationToken);
    Task UpdateAsync(PaymentCard paymentCard, CancellationToken cancellationToken);
    Task DeleteAsync(PaymentCard paymentCard, CancellationToken cancellationToken);
}
