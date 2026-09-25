
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Screening
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ScreeningId { get; set; } 
 public virtual RiskScore? Score { get; set; } 
 public virtual DateTime? ScreenedAt { get; set; } 
public virtual KYCProfile? KycProfile { get; set; } 
public virtual ICollection<ComplianceAlert> Alerts { get; set; } = new List<ComplianceAlert>();
 public virtual ScreeningType? ScreeningType { get; set; } 
 public virtual ScreeningStatus? Status { get; set; } 

    public static Screening FromRequest(ScreeningRequest request) {
        return new Screening {
            Id = request.Id,
            Score = request.Score,
            ScreenedAt = request.ScreenedAt,
            ScreeningType = request.ScreeningType,
            Status = request.Status,
        };
    }
}
