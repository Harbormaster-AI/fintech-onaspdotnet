
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class KYCProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? KycprofileId { get; set; } 
 public virtual string? ProfileId { get; set; } 
 public virtual DateTime? CreatedAt { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual ICollection<KYCDocument> Documents { get; set; } = new List<KYCDocument>();
public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();
public virtual ICollection<VerifiedAddress> Addresses { get; set; } = new List<VerifiedAddress>();
 public virtual KYCStatus? Status { get; set; } 
 public virtual VerificationLevel? VerificationLevel { get; set; } 

    public static KYCProfile FromRequest(KYCProfileRequest request) {
        return new KYCProfile {
            Id = request.Id,
            ProfileId = request.ProfileId,
            CreatedAt = request.CreatedAt,
            Status = request.Status,
            VerificationLevel = request.VerificationLevel,
        };
    }
}
