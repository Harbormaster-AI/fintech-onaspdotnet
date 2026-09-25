
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Payout
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PayoutId { get; set; } 
 public virtual string? PayoutReference { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual string? Currency { get; set; } 
 public virtual DateOnly? ScheduledDate { get; set; } 
 public virtual DateOnly? PaidDate { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual SettlementBatch? SettlementBatch { get; set; } 
public virtual Account? DestinationAccount { get; set; } 
 public virtual PayoutStatus? Status { get; set; } 

    public static Payout FromRequest(PayoutRequest request) {
        return new Payout {
            Id = request.Id,
            PayoutReference = request.PayoutReference,
            Amount = request.Amount,
            Currency = request.Currency,
            ScheduledDate = request.ScheduledDate,
            PaidDate = request.PaidDate,
            Status = request.Status,
        };
    }
}
