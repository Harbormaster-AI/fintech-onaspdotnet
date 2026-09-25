
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Agreement
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AgreementId { get; set; } 
 public virtual string? AgreementNumber { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual ProductOffering? ProductOffering { get; set; } 
 public virtual AgreementType? AgreementType { get; set; } 
 public virtual AgreementStatus? Status { get; set; } 

    public static Agreement FromRequest(AgreementRequest request) {
        return new Agreement {
            Id = request.Id,
            AgreementNumber = request.AgreementNumber,
            EffectiveDate = request.EffectiveDate,
            AgreementType = request.AgreementType,
            Status = request.Status,
        };
    }
}
