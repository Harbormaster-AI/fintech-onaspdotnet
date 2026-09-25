using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IPaymentProcessorRepository
{
    Task<PaymentProcessor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentProcessor>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken);
    Task UpdateAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken);
    Task DeleteAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken);

    Task AddToInstitutionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInstitutionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSettlementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSettlementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
