
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class PaymentOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PaymentorderId { get; set; } 
 public virtual string? OrderReference { get; set; } 
 public virtual DateOnly? RequestedExecutionDate { get; set; } 
 public virtual string? Purpose { get; set; } 
public virtual Account? SourceAccount { get; set; } 
public virtual Account? DestinationAccount { get; set; } 
public virtual Beneficiary? Beneficiary { get; set; } 
public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
public virtual FXDeal? FxDeal { get; set; } 
public virtual ICollection<AppliedFee> Fees { get; set; } = new List<AppliedFee>();
 public virtual PaymentMethod? PaymentMethod { get; set; } 
 public virtual PaymentOrderStatus? Status { get; set; } 
 public virtual PaymentPriority? Priority { get; set; } 

    public static PaymentOrder FromRequest(PaymentOrderRequest request) {
        return new PaymentOrder {
            Id = request.Id,
            OrderReference = request.OrderReference,
            RequestedExecutionDate = request.RequestedExecutionDate,
            Purpose = request.Purpose,
            PaymentMethod = request.PaymentMethod,
            Status = request.Status,
            Priority = request.Priority,
        };
    }
}
