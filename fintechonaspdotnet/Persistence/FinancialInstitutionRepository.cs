
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class FinancialInstitutionRepository : IFinancialInstitutionRepository
{
    private readonly ApplicationDbContext _db;

    public FinancialInstitutionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<FinancialInstitution?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.FinancialInstitutions
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FinancialInstitution>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.FinancialInstitutions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken)
    {
        _db.FinancialInstitutions.Add(financialInstitution);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken)
    {
        _db.FinancialInstitutions.Update(financialInstitution);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken)
    {
        _db.FinancialInstitutions.Remove(financialInstitution);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToBranchesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Branchs
            .Where(branch =>
                request.ChildIds.Contains(branch.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    branch =>
                        EF.Property<Guid?>(
                            branch,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBranchesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Branchs
            .Where(branch =>
                request.ChildIds.Contains(branch.Id) &&
                EF.Property<Guid?>(
                    branch,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    branch =>
                        EF.Property<Guid?>(
                            branch,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToCustomersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Customers
            .Where(customer =>
                request.ChildIds.Contains(customer.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    customer =>
                        EF.Property<Guid?>(
                            customer,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCustomersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Customers
            .Where(customer =>
                request.ChildIds.Contains(customer.Id) &&
                EF.Property<Guid?>(
                    customer,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    customer =>
                        EF.Property<Guid?>(
                            customer,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToProductOfferingsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductOfferings
            .Where(productOffering =>
                request.ChildIds.Contains(productOffering.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productOffering =>
                        EF.Property<Guid?>(
                            productOffering,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProductOfferingsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductOfferings
            .Where(productOffering =>
                request.ChildIds.Contains(productOffering.Id) &&
                EF.Property<Guid?>(
                    productOffering,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productOffering =>
                        EF.Property<Guid?>(
                            productOffering,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToPaymentProcessorsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentProcessors
            .Where(paymentProcessor =>
                request.ChildIds.Contains(paymentProcessor.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentProcessor =>
                        EF.Property<Guid?>(
                            paymentProcessor,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPaymentProcessorsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentProcessors
            .Where(paymentProcessor =>
                request.ChildIds.Contains(paymentProcessor.Id) &&
                EF.Property<Guid?>(
                    paymentProcessor,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentProcessor =>
                        EF.Property<Guid?>(
                            paymentProcessor,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToCompliancePoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CompliancePolicys
            .Where(compliancePolicy =>
                request.ChildIds.Contains(compliancePolicy.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    compliancePolicy =>
                        EF.Property<Guid?>(
                            compliancePolicy,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCompliancePoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CompliancePolicys
            .Where(compliancePolicy =>
                request.ChildIds.Contains(compliancePolicy.Id) &&
                EF.Property<Guid?>(
                    compliancePolicy,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    compliancePolicy =>
                        EF.Property<Guid?>(
                            compliancePolicy,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
