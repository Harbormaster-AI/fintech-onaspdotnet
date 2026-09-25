
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Chargeback
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ChargebackId { get; set; } 
 public virtual string? ChargebackReference { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateTime? PostedAt { get; set; } 
public virtual Dispute? Dispute { get; set; } 
public virtual Transaction? Transaction { get; set; } 
 public virtual ChargebackStage? Stage { get; set; } 
 public virtual ChargebackStatus? Status { get; set; } 

    public static Chargeback FromRequest(ChargebackRequest request) {
        return new Chargeback {
            Id = request.Id,
            ChargebackReference = request.ChargebackReference,
            Amount = request.Amount,
            PostedAt = request.PostedAt,
            Stage = request.Stage,
            Status = request.Status,
        };
    }
}
