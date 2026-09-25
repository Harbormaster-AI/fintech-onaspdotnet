
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class PaymentContract
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PaymentcontractId { get; set; } 
 public virtual string? ContractNumber { get; set; } 
 public virtual string? PricingPlanCode { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual PaymentProcessor? Acquirer { get; set; } 
 public virtual ContractStatus? Status { get; set; } 

    public static PaymentContract FromRequest(PaymentContractRequest request) {
        return new PaymentContract {
            Id = request.Id,
            ContractNumber = request.ContractNumber,
            PricingPlanCode = request.PricingPlanCode,
            Status = request.Status,
        };
    }
}
