
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Branch
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BranchId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? BranchCode { get; set; } 
 public virtual Address? Address { get; set; } 
public virtual FinancialInstitution? Institution { get; set; } 

    public static Branch FromRequest(BranchRequest request) {
        return new Branch {
            Id = request.Id,
            Name = request.Name,
            BranchCode = request.BranchCode,
            Address = request.Address,
        };
    }
}
