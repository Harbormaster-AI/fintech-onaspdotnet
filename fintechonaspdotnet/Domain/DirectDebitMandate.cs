
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class DirectDebitMandate
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DirectdebitmandateId { get; set; } 
 public virtual string? MandateId { get; set; } 
 public virtual DateTime? SignedAt { get; set; } 
public virtual Account? Account { get; set; } 
public virtual Creditor? Creditor { get; set; } 
 public virtual DirectDebitScheme? Scheme { get; set; } 
 public virtual MandateStatus? Status { get; set; } 

    public static DirectDebitMandate FromRequest(DirectDebitMandateRequest request) {
        return new DirectDebitMandate {
            Id = request.Id,
            MandateId = request.MandateId,
            SignedAt = request.SignedAt,
            Scheme = request.Scheme,
            Status = request.Status,
        };
    }
}
