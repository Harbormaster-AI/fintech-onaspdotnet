
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Dispute
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DisputeId { get; set; } 
 public virtual string? DisputeReference { get; set; } 
 public virtual DateTime? OpenedAt { get; set; } 
 public virtual DateTime? ClosedAt { get; set; } 
public virtual Transaction? Transaction { get; set; } 
public virtual PaymentCard? Card { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<Chargeback> Chargebacks { get; set; } = new List<Chargeback>();
 public virtual DisputeReason? Reason { get; set; } 
 public virtual DisputeStatus? Status { get; set; } 

    public static Dispute FromRequest(DisputeRequest request) {
        return new Dispute {
            Id = request.Id,
            DisputeReference = request.DisputeReference,
            OpenedAt = request.OpenedAt,
            ClosedAt = request.ClosedAt,
            Reason = request.Reason,
            Status = request.Status,
        };
    }
}
