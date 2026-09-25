
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class APIClient
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ApiclientId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ClientId { get; set; } 
 public virtual string? RedirectUri { get; set; } 
public virtual ICollection<Consent> Consents { get; set; } = new List<Consent>();
 public virtual ClientType? ClientType { get; set; } 

    public static APIClient FromRequest(APIClientRequest request) {
        return new APIClient {
            Id = request.Id,
            Name = request.Name,
            ClientId = request.ClientId,
            RedirectUri = request.RedirectUri,
            ClientType = request.ClientType,
        };
    }
}
