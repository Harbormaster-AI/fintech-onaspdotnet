
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace fintechonaspdotnet.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _db;

    public CustomerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Customers
            .Include(x => x.Institution)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Customers
            .AsNoTracking()
            .Include(x => x.Institution)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Update(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAccountsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Accounts
            .Where(account =>
                request.ChildIds.Contains(account.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    account =>
                        EF.Property<Guid?>(
                            account,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAccountsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Accounts
            .Where(account =>
                request.ChildIds.Contains(account.Id) &&
                EF.Property<Guid?>(
                    account,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    account =>
                        EF.Property<Guid?>(
                            account,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToWalletsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Wallets
            .Where(wallet =>
                request.ChildIds.Contains(wallet.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    wallet =>
                        EF.Property<Guid?>(
                            wallet,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWalletsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Wallets
            .Where(wallet =>
                request.ChildIds.Contains(wallet.Id) &&
                EF.Property<Guid?>(
                    wallet,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    wallet =>
                        EF.Property<Guid?>(
                            wallet,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToCardsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentCards
            .Where(paymentCard =>
                request.ChildIds.Contains(paymentCard.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentCard =>
                        EF.Property<Guid?>(
                            paymentCard,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCardsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PaymentCards
            .Where(paymentCard =>
                request.ChildIds.Contains(paymentCard.Id) &&
                EF.Property<Guid?>(
                    paymentCard,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    paymentCard =>
                        EF.Property<Guid?>(
                            paymentCard,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToKycProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.KYCProfiles
            .Where(kYCProfile =>
                request.ChildIds.Contains(kYCProfile.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    kYCProfile =>
                        EF.Property<Guid?>(
                            kYCProfile,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromKycProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.KYCProfiles
            .Where(kYCProfile =>
                request.ChildIds.Contains(kYCProfile.Id) &&
                EF.Property<Guid?>(
                    kYCProfile,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    kYCProfile =>
                        EF.Property<Guid?>(
                            kYCProfile,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToConsentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Consents
            .Where(consent =>
                request.ChildIds.Contains(consent.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    consent =>
                        EF.Property<Guid?>(
                            consent,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromConsentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Consents
            .Where(consent =>
                request.ChildIds.Contains(consent.Id) &&
                EF.Property<Guid?>(
                    consent,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    consent =>
                        EF.Property<Guid?>(
                            consent,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToAgreementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Agreements
            .Where(agreement =>
                request.ChildIds.Contains(agreement.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    agreement =>
                        EF.Property<Guid?>(
                            agreement,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAgreementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Agreements
            .Where(agreement =>
                request.ChildIds.Contains(agreement.Id) &&
                EF.Property<Guid?>(
                    agreement,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    agreement =>
                        EF.Property<Guid?>(
                            agreement,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToLoanApplicationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LoanApplications
            .Where(loanApplication =>
                request.ChildIds.Contains(loanApplication.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    loanApplication =>
                        EF.Property<Guid?>(
                            loanApplication,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLoanApplicationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LoanApplications
            .Where(loanApplication =>
                request.ChildIds.Contains(loanApplication.Id) &&
                EF.Property<Guid?>(
                    loanApplication,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    loanApplication =>
                        EF.Property<Guid?>(
                            loanApplication,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToLoansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Loans
            .Where(loan =>
                request.ChildIds.Contains(loan.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    loan =>
                        EF.Property<Guid?>(
                            loan,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLoansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Loans
            .Where(loan =>
                request.ChildIds.Contains(loan.Id) &&
                EF.Property<Guid?>(
                    loan,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    loan =>
                        EF.Property<Guid?>(
                            loan,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToPortfoliosAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InvestmentPortfolios
            .Where(investmentPortfolio =>
                request.ChildIds.Contains(investmentPortfolio.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    investmentPortfolio =>
                        EF.Property<Guid?>(
                            investmentPortfolio,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPortfoliosAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InvestmentPortfolios
            .Where(investmentPortfolio =>
                request.ChildIds.Contains(investmentPortfolio.Id) &&
                EF.Property<Guid?>(
                    investmentPortfolio,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    investmentPortfolio =>
                        EF.Property<Guid?>(
                            investmentPortfolio,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }


    public async Task AddToDisputesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Disputes
            .Where(dispute =>
                request.ChildIds.Contains(dispute.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dispute =>
                        EF.Property<Guid?>(
                            dispute,
                            "ExchangeRate_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDisputesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Disputes
            .Where(dispute =>
                request.ChildIds.Contains(dispute.Id) &&
                EF.Property<Guid?>(
                    dispute,
                    "ExchangeRate_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dispute =>
                        EF.Property<Guid?>(
                            dispute,
                            "ExchangeRate_Id"),
                    (Guid?)null));
    }

}
