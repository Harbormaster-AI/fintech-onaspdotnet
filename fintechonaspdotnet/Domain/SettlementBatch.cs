
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class SettlementBatch
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SettlementbatchId { get; set; } 
 public virtual string? BatchId { get; set; } 
 public virtual DateTime? PeriodStart { get; set; } 
 public virtual DateTime? PeriodEnd { get; set; } 
 public virtual Money? TotalVolume { get; set; } 
 public virtual int? TotalCount { get; set; } 
public virtual PaymentProcessor? Processor { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();
public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
 public virtual SettlementStatus? Status { get; set; } 

    public static SettlementBatch FromRequest(SettlementBatchRequest request) {
        return new SettlementBatch {
            Id = request.Id,
            BatchId = request.BatchId,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            TotalVolume = request.TotalVolume,
            TotalCount = request.TotalCount,
            Status = request.Status,
        };
    }
}
