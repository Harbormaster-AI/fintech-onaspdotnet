
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class AppliedFee
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AppliedfeeId { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual string? Description { get; set; } 
public virtual PaymentOrder? PaymentOrder { get; set; } 
public virtual Transaction? Transaction { get; set; } 
 public virtual FeeType? FeeType { get; set; } 

    public static AppliedFee FromRequest(AppliedFeeRequest request) {
        return new AppliedFee {
            Id = request.Id,
            Amount = request.Amount,
            Description = request.Description,
            FeeType = request.FeeType,
        };
    }
}
