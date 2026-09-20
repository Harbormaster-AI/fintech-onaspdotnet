using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IPaymentProcessorRepository
{
    Task<PaymentProcessor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentProcessor>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken);
    Task UpdateAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken);
    Task DeleteAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken);
}
