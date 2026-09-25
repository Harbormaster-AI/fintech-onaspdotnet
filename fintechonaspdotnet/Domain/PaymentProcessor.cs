
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class PaymentProcessor
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PaymentprocessorId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ProcessorCode { get; set; } 
 public virtual string? NetworkSupport { get; set; } 
public virtual ICollection<FinancialInstitution> Institutions { get; set; } = new List<FinancialInstitution>();
public virtual ICollection<PaymentContract> Contracts { get; set; } = new List<PaymentContract>();
public virtual ICollection<SettlementBatch> Settlements { get; set; } = new List<SettlementBatch>();

    public static PaymentProcessor FromRequest(PaymentProcessorRequest request) {
        return new PaymentProcessor {
            Id = request.Id,
            Name = request.Name,
            ProcessorCode = request.ProcessorCode,
            NetworkSupport = request.NetworkSupport,
        };
    }
}
