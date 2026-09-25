
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Collateral
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CollateralId { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual Money? Value { get; set; } 
public virtual Loan? Loan { get; set; } 
 public virtual CollateralType? CollateralType { get; set; } 

    public static Collateral FromRequest(CollateralRequest request) {
        return new Collateral {
            Id = request.Id,
            Description = request.Description,
            Value = request.Value,
            CollateralType = request.CollateralType,
        };
    }
}
