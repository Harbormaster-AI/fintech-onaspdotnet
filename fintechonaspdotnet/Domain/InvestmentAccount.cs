
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class InvestmentAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InvestmentaccountId { get; set; } 
 public virtual AccountNumber? AccountNumber { get; set; } 
 public virtual string? BaseCurrency { get; set; } 
 public virtual Money? Balance { get; set; } 
public virtual InvestmentPortfolio? Portfolio { get; set; } 
public virtual ICollection<Trade> Trades { get; set; } = new List<Trade>();
public virtual ICollection<TradeOrder> Orders { get; set; } = new List<TradeOrder>();
 public virtual InvestmentAccountType? AccountType { get; set; } 
 public virtual AccountStatus? Status { get; set; } 

    public static InvestmentAccount FromRequest(InvestmentAccountRequest request) {
        return new InvestmentAccount {
            Id = request.Id,
            AccountNumber = request.AccountNumber,
            BaseCurrency = request.BaseCurrency,
            Balance = request.Balance,
            AccountType = request.AccountType,
            Status = request.Status,
        };
    }
}
