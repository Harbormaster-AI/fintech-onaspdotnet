
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class PricingPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PricingplanId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? PlanCode { get; set; } 
 public virtual string? BaseCurrency { get; set; } 
public virtual ProductOffering? ProductOffering { get; set; } 
public virtual ICollection<FeeSchedule> FeeSchedules { get; set; } = new List<FeeSchedule>();
public virtual ICollection<UsageLimit> Limits { get; set; } = new List<UsageLimit>();
 public virtual PlanStatus? Status { get; set; } 

    public static PricingPlan FromRequest(PricingPlanRequest request) {
        return new PricingPlan {
            Id = request.Id,
            Name = request.Name,
            PlanCode = request.PlanCode,
            BaseCurrency = request.BaseCurrency,
            Status = request.Status,
        };
    }
}
