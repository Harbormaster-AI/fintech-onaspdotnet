
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class VerifiedAddress
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? VerifiedaddressId { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual DateTime? VerifiedAt { get; set; } 
public virtual KYCProfile? KycProfile { get; set; } 
 public virtual VerificationStatus? VerificationStatus { get; set; } 

    public static VerifiedAddress FromRequest(VerifiedAddressRequest request) {
        return new VerifiedAddress {
            Id = request.Id,
            Address = request.Address,
            VerifiedAt = request.VerifiedAt,
            VerificationStatus = request.VerificationStatus,
        };
    }
}
