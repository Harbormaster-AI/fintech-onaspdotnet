
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class AccountStatement
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AccountstatementId { get; set; } 
 public virtual string? StatementNumber { get; set; } 
 public virtual DateOnly? PeriodStart { get; set; } 
 public virtual DateOnly? PeriodEnd { get; set; } 
 public virtual Money? OpeningBalance { get; set; } 
 public virtual Money? ClosingBalance { get; set; } 
 public virtual DateTime? GeneratedAt { get; set; } 
public virtual Account? Account { get; set; } 

    public static AccountStatement FromRequest(AccountStatementRequest request) {
        return new AccountStatement {
            Id = request.Id,
            StatementNumber = request.StatementNumber,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            OpeningBalance = request.OpeningBalance,
            ClosingBalance = request.ClosingBalance,
            GeneratedAt = request.GeneratedAt,
        };
    }
}
