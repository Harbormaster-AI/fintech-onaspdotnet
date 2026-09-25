
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class CompliancePolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CompliancepolicyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? PolicyCode { get; set; } 
 public virtual string? Description { get; set; } 
public virtual FinancialInstitution? Institution { get; set; } 
 public virtual PolicyStatus? Status { get; set; } 

    public static CompliancePolicy FromRequest(CompliancePolicyRequest request) {
        return new CompliancePolicy {
            Id = request.Id,
            Name = request.Name,
            PolicyCode = request.PolicyCode,
            Description = request.Description,
            Status = request.Status,
        };
    }
}
