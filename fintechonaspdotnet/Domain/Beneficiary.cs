
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Beneficiary
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BeneficiaryId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual AccountIdentifier? AccountIdentifier { get; set; } 
 public virtual IBAN? Iban { get; set; } 
 public virtual BIC? Bic { get; set; } 
 public virtual Address? Address { get; set; } 
public virtual Customer? Customer { get; set; } 

    public static Beneficiary FromRequest(BeneficiaryRequest request) {
        return new Beneficiary {
            Id = request.Id,
            Name = request.Name,
            AccountIdentifier = request.AccountIdentifier,
            Iban = request.Iban,
            Bic = request.Bic,
            Address = request.Address,
        };
    }
}
