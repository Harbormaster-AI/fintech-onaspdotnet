using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IPaymentContractRepository
{
    Task<PaymentContract?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentContract>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PaymentContract paymentContract, CancellationToken cancellationToken);
    Task UpdateAsync(PaymentContract paymentContract, CancellationToken cancellationToken);
    Task DeleteAsync(PaymentContract paymentContract, CancellationToken cancellationToken);


}
