
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class PaymentProcessorRepository : IPaymentProcessorRepository
{
    private readonly ApplicationDbContext _db;

    public PaymentProcessorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PaymentProcessor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PaymentProcessors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PaymentProcessor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PaymentProcessors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken)
    {
        _db.PaymentProcessors.Add(paymentProcessor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken)
    {
        _db.PaymentProcessors.Update(paymentProcessor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentProcessor paymentProcessor, CancellationToken cancellationToken)
    {
        _db.PaymentProcessors.Remove(paymentProcessor);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToInstitutionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FinancialInstitutions
            .Where(financialInstitution =>
                request.ChildIds.Contains(financialInstitution.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    financialInstitution =>
                        EF.Property<Guid?>(
                            financialInstitution,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInstitutionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FinancialInstitutions
            .Where(financialInstitution =>
                request.ChildIds.Contains(financialInstitution.Id) &&
                EF.Property<Guid?>(
                    financialInstitution,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    financialInstitution =>
                        EF.Property<Guid?>(
                            financialInstitution,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToContractsAsync(
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

    public async Task RemoveFromContractsAsync(
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

}
