
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Creditor
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CreditorId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual BIC? Bic { get; set; } 
 public virtual Address? Address { get; set; } 
public virtual ICollection<DirectDebitMandate> Mandates { get; set; } = new List<DirectDebitMandate>();

    public static Creditor FromRequest(CreditorRequest request) {
        return new Creditor {
            Id = request.Id,
            Name = request.Name,
            Bic = request.Bic,
            Address = request.Address,
        };
    }
}
