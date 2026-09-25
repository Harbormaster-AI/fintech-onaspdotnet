
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class UsageLimit
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? UsagelimitId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual int? Count { get; set; } 
public virtual PricingPlan? PricingPlan { get; set; } 
 public virtual LimitScope? Scope { get; set; } 
 public virtual LimitPeriod? Period { get; set; } 

    public static UsageLimit FromRequest(UsageLimitRequest request) {
        return new UsageLimit {
            Id = request.Id,
            Name = request.Name,
            Amount = request.Amount,
            Count = request.Count,
            Scope = request.Scope,
            Period = request.Period,
        };
    }
}
