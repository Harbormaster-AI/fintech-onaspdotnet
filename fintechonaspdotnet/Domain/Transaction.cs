
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TransactionId { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual Money? Fee { get; set; } 
 public virtual decimal? ExchangeRate { get; set; } 
 public virtual DateTime? CreatedAt { get; set; } 
 public virtual DateTime? CompletedAt { get; set; } 
 public virtual string? Narrative { get; set; } 
public virtual Account? Account { get; set; } 
public virtual Wallet? Wallet { get; set; } 
public virtual PaymentOrder? PaymentOrder { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual PaymentCard? Card { get; set; } 
public virtual ICollection<Transaction> RelatedTransactions { get; set; } = new List<Transaction>();
public virtual ICollection<ComplianceAlert> Alerts { get; set; } = new List<ComplianceAlert>();
 public virtual TransactionType? TransactionType { get; set; } 
 public virtual TransactionStatus? Status { get; set; } 

    public static Transaction FromRequest(TransactionRequest request) {
        return new Transaction {
            Id = request.Id,
            Amount = request.Amount,
            Fee = request.Fee,
            ExchangeRate = request.ExchangeRate,
            CreatedAt = request.CreatedAt,
            CompletedAt = request.CompletedAt,
            Narrative = request.Narrative,
            TransactionType = request.TransactionType,
            Status = request.Status,
        };
    }
}
