
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class LoanTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LoantransactionId { get; set; } 
 public virtual TransactionId? TransactionId { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? PostingDate { get; set; } 
public virtual Loan? Loan { get; set; } 
 public virtual LoanTransactionType? Type { get; set; } 
 public virtual PostingStatus? Status { get; set; } 

    public static LoanTransaction FromRequest(LoanTransactionRequest request) {
        return new LoanTransaction {
            Id = request.Id,
            TransactionId = request.TransactionId,
            Amount = request.Amount,
            PostingDate = request.PostingDate,
            Type = request.Type,
            Status = request.Status,
        };
    }
}
