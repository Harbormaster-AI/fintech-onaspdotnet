
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class FeeSchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? FeescheduleId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual decimal? Percentage { get; set; } 
 public virtual Money? Minimum { get; set; } 
 public virtual Money? Maximum { get; set; } 
public virtual PricingPlan? PricingPlan { get; set; } 
 public virtual FeeType? FeeType { get; set; } 
 public virtual FeeCalculationMethod? CalculationMethod { get; set; } 

    public static FeeSchedule FromRequest(FeeScheduleRequest request) {
        return new FeeSchedule {
            Id = request.Id,
            Name = request.Name,
            Amount = request.Amount,
            Percentage = request.Percentage,
            Minimum = request.Minimum,
            Maximum = request.Maximum,
            FeeType = request.FeeType,
            CalculationMethod = request.CalculationMethod,
        };
    }
}
