
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CustomerId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual DateOnly? DateOfBirth { get; set; } 
 public virtual Email? Email { get; set; } 
 public virtual PhoneNumber? Phone { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual TaxId? TaxId { get; set; } 
 public virtual RiskScore? RiskScore { get; set; } 
public virtual FinancialInstitution? Institution { get; set; } 
public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
public virtual ICollection<PaymentCard> Cards { get; set; } = new List<PaymentCard>();
public virtual ICollection<KYCProfile> KycProfiles { get; set; } = new List<KYCProfile>();
public virtual ICollection<Consent> Consents { get; set; } = new List<Consent>();
public virtual ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();
public virtual ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();
public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
public virtual ICollection<InvestmentPortfolio> Portfolios { get; set; } = new List<InvestmentPortfolio>();
public virtual ICollection<Dispute> Disputes { get; set; } = new List<Dispute>();
 public virtual CustomerType? CustomerType { get; set; } 

    public static Customer FromRequest(CustomerRequest request) {
        return new Customer {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            TaxId = request.TaxId,
            RiskScore = request.RiskScore,
            CustomerType = request.CustomerType,
        };
    }
}
