
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Consent
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ConsentId { get; set; } 
 public virtual DateTime? GrantedAt { get; set; } 
 public virtual DateTime? ExpiresAt { get; set; } 
 public virtual string? Scope { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual APIClient? ApiClient { get; set; } 
 public virtual ConsentType? ConsentType { get; set; } 
 public virtual ConsentStatus? Status { get; set; } 

    public static Consent FromRequest(ConsentRequest request) {
        return new Consent {
            Id = request.Id,
            GrantedAt = request.GrantedAt,
            ExpiresAt = request.ExpiresAt,
            Scope = request.Scope,
            ConsentType = request.ConsentType,
            Status = request.Status,
        };
    }
}
