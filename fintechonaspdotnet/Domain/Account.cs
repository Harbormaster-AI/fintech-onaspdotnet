
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AccountId { get; set; } 
 public virtual AccountNumber? AccountNumber { get; set; } 
 public virtual IBAN? Iban { get; set; } 
 public virtual BIC? Bic { get; set; } 
 public virtual DateOnly? OpenedDate { get; set; } 
 public virtual string? Currency { get; set; } 
 public virtual Money? Balance { get; set; } 
 public virtual Money? AvailableBalance { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual FinancialInstitution? Institution { get; set; } 
public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
public virtual ICollection<PaymentCard> Cards { get; set; } = new List<PaymentCard>();
public virtual ICollection<AccountStatement> Statements { get; set; } = new List<AccountStatement>();
public virtual ICollection<DirectDebitMandate> Mandates { get; set; } = new List<DirectDebitMandate>();
 public virtual AccountType? AccountType { get; set; } 
 public virtual AccountStatus? Status { get; set; } 

    public static Account FromRequest(AccountRequest request) {
        return new Account {
            Id = request.Id,
            AccountNumber = request.AccountNumber,
            Iban = request.Iban,
            Bic = request.Bic,
            OpenedDate = request.OpenedDate,
            Currency = request.Currency,
            Balance = request.Balance,
            AvailableBalance = request.AvailableBalance,
            AccountType = request.AccountType,
            Status = request.Status,
        };
    }
}
