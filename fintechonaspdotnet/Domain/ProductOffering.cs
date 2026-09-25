
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class ProductOffering
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProductofferingId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ProductCode { get; set; } 
public virtual FinancialInstitution? Institution { get; set; } 
public virtual ICollection<PricingPlan> PricingPlans { get; set; } = new List<PricingPlan>();
 public virtual ProductCategory? Category { get; set; } 

    public static ProductOffering FromRequest(ProductOfferingRequest request) {
        return new ProductOffering {
            Id = request.Id,
            Name = request.Name,
            ProductCode = request.ProductCode,
            Category = request.Category,
        };
    }
}
